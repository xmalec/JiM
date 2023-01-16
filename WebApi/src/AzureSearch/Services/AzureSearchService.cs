using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using AzureSearch.Models;
using Microsoft.Extensions.Logging;

namespace AzureSearch.Services;
public abstract class AzureSearchService<TResult, TQuery>
    : IAzureSearchService<TResult, TQuery>
    where TResult : BaseIndexModel
    where TQuery : QueryBaseModel
{

    private readonly ILogger<AzureSearchService<TResult, TQuery>> logger;
    private readonly SearchIndexClient indexClient;
    protected SearchClient searchClient;

    protected AzureSearchService(ILogger<AzureSearchService<TResult, TQuery>> logger, SearchIndexClient indexClient, SearchClient searchClient)
    {
        this.logger = logger;
        this.indexClient = indexClient;
        this.searchClient = searchClient;
    }
    public abstract Task<AzureSearchResult<TResult>> Search(TQuery query, CancellationToken cancellationToken = default);
    protected virtual void CustomizeIndex(SearchIndex definition)
    {
        return;
    }
    public async Task<bool> RebuildIndex()
    {
        await DeleteIndexIfExists();
        await CreateIndex();
        return await IndexAll();
    }

    protected abstract Task<bool> IndexAll();
    protected abstract string GetFilter(TQuery query, string? exceptFilter = null);
    protected async Task<AzureSearchResult<TResult>> SearchInternal(TQuery query, CancellationToken cancellationToken = default)
    {
        var searchResults = await searchClient.SearchAsync<TResult>(query.SearchString, CreateSearchOptions(query), cancellationToken);
        var searchResultsComplete = await searchResults.Value.GetResultsAsync().ToListAsync(cancellationToken: cancellationToken);

        var totalResultCount = searchResults.Value.TotalCount ?? 0;
        var facets = await GetFacetResult(query, searchResults.Value.Facets);
        return new AzureSearchResult<TResult>
        {
            Results = searchResultsComplete.Select(r => r.Document).ToList(),
            PageCount = (long)Math.Ceiling(totalResultCount / (double)query.Take),
            PageIndex = (query.Skip / query.Take) + 1,
            PageSize = query.Take,
            TotalCount = totalResultCount,
            Facets = facets
        };
    }

    protected async Task CreateIndex()
    {
        FieldBuilder fieldBuilder = new FieldBuilder();
        try
        {

            var searchFields = fieldBuilder.Build(typeof(TResult));
            var definition = new SearchIndex(searchClient.IndexName, searchFields);
            CustomizeIndex(definition);
            await indexClient.CreateOrUpdateIndexAsync(definition);
        }
        catch (RequestFailedException e)
        {
            logger.LogError(e, "Failed to create index {IndexName}", searchClient.IndexName);
            throw;
        }
    }

    protected async Task<bool> AddToIndex(IEnumerable<TResult> elements)
    {
        var actions = new List<IndexDocumentsAction<TResult>>();
        foreach (var element in elements)
        {
            actions.Add(IndexDocumentsAction.MergeOrUpload(element));
        }

        var batch = IndexDocumentsBatch.Create(actions.ToArray());
        try
        {
            await CreateIndex();
            IndexDocumentsResult result = await searchClient.IndexDocumentsAsync(batch);
            return true;
        }
        catch (Exception e)
        {
            // Sometimes when your Search service is under load, indexing will fail for some of the documents in the batch
            logger.LogWarning(e, "Add to search index failed {IndexName}, IDs: [{Ids}]",
                searchClient.IndexName, string.Join(", ", elements.Select(x => x.Id).ToList()));
            return false;
        }
    }



    private SearchOptions CreateSearchOptions(TQuery query)
    {
        var searchOptions = new SearchOptions
        {
            QueryType = SearchQueryType.Full,
            IncludeTotalCount = true,
            Size = query.Take,
            Skip = query.Skip,
            Filter = GetFilter(query)
        };
        foreach (var select in query.Selects)
        {
            searchOptions.Select.Add(select);
        }
        foreach (var orderBy in query.OrderBys)
        {
            searchOptions.OrderBy.Add(orderBy);
        }
        foreach (var facet in query.DividedFilters.Unselected)
        {
            searchOptions.Facets.Add($"{facet},count:100");
        }
        return searchOptions;
    }

    private async Task DeleteIndexIfExists()
    {
        try
        {
            await indexClient.DeleteIndexAsync(searchClient.IndexName);
        }
        catch (RequestFailedException e) when (e.Status == 404)
        {
            logger.LogError(e, "Index to delete {IndexName} has not been found",
                searchClient.IndexName);
            throw;
        }
    }

    private async Task<List<FacetValue>> GetFacets(TQuery query, IList<string> selectedFilters)
    {
        var tasks = new List<Task<List<FacetValue>>>();
        foreach (var facet in selectedFilters)
        {
            var filter = GetFilter(query, facet);
            tasks.Add(SendRequest(filter, facet, query.SearchString));
        }

        var results = await Task.WhenAll(tasks);

        return results.SelectMany(r => r).ToList();
    }

    private async Task<List<FacetValue>> SendRequest(string filter, string neededFacet, string searchText = "")
    {
        var searchOptions = new SearchOptions
        {
            Select = { "Id" },
            Filter = filter,
            QueryType = SearchQueryType.Full
        };
        //searchOptions.QueryType = SearchQueryType.Full;
        searchOptions.Facets.Add($"{neededFacet},count:100");
        var response = await searchClient.SearchAsync<TResult>(searchText, searchOptions);
        return response.Value.Facets.First().Value
            .Select(f => new FacetValue
            {
                Name = neededFacet,
                Count = (int)(f.Count ?? 0),
                Value = f.Value
            })
            .ToList();
    }

    private static ILookup<string, FacetValue> MergeFacets(IDictionary<string, IList<FacetResult>> resultFacets, List<FacetValue> facets)
    {
        var facetsSet = new HashSet<string>(facets.Select(f => f.Name));
        foreach (var facet in resultFacets)
        {
            if (!facetsSet.Contains(facet.Key))
            {
                facets.AddRange(facet.Value.Select(f => new FacetValue
                {
                    Value = f.Value,
                    Count = (int)f.Count,
                    Name = facet.Key
                }));
            }
        }
        var facetsLookup = facets.ToLookup(f => f.Name);
        return facetsLookup;
    }

    private async Task<ILookup<string, FacetValue>> GetFacetResult(TQuery query, IDictionary<string, IList<FacetResult>>? facetResults)
    {
        facetResults = facetResults ?? new Dictionary<string, IList<FacetResult>>();
        var facets = await GetFacets(query, query.DividedFilters.Selected);
        return MergeFacets(facetResults, facets);
    }

    public async Task<bool> RemoveFromIndex(IList<TResult> toRemove)
    {
        if (toRemove is null || toRemove.Count == 0)
        {
            return true;
        }
        var actions = toRemove
            .Select(post => IndexDocumentsAction.Delete(post))
            .ToArray();
        IndexDocumentsBatch<TResult> batch = IndexDocumentsBatch.Create(actions);
        try
        {
            IndexDocumentsResult result = await searchClient.IndexDocumentsAsync(batch);
            return true;
        }
        catch (Exception e)
        {
            // Sometimes when your Search service is under load, indexing will fail for some of the documents in the batch.
            logger.LogWarning(e, "Remove from search index failed. Index name: {IndexNames}," +
                " failed index ids: [{Ids}]", searchClient.IndexName, string.Join(", ", toRemove.Select(x => x.Id).ToList()));
            return false;
        }
    }
}

