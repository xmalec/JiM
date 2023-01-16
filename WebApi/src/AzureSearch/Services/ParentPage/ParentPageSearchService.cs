using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using AzureSearch.AzureSearchFilterBuilder;
using AzureSearch.Models;
using AzureSearch.Models.ParentPage;
using BL.Models.ParentPage;
using BL.Services.ParentPage;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AzureSearch.Services.ParentPage;
public class ParentPageSearchService : AzureSearchService<ParentPageIndexModel, ParentPageQueryModel>, IParentPageSearchService
{
    private readonly IMemoryCache memoryCache;
    private readonly IParentPageService parentPageService;

    public const string AnalyzerName = "jobRequisitionAnalyzer";
    public ParentPageSearchService(
            IAzureClientFactory<SearchClient> clientFactory,
            ILogger<ParentPageSearchService> logger,
            SearchIndexClient indexClient,
            IMemoryCache memoryCache
,
            IParentPageService parentPageService)
        : base(logger, indexClient, clientFactory.CreateClient(ServiceRegistration.ParentPageClient))
    {
        this.memoryCache = memoryCache;
        this.parentPageService = parentPageService;
    }

    protected override async Task<bool> IndexAll()
    {
        var parentPages = parentPageService.GetAllIndexModelsAsync();
        return await AddToIndex(ConvertToIndexModels(parentPages));
    }

    private IList<ParentPageIndexModel> ConvertToIndexModels(IList<ParentPageIndexModelDto> jobRequisitions)
    {
        return jobRequisitions.Select(pp => new ParentPageIndexModel()
        {
            Id = pp.Id.ToString(),
            Title = pp.Title,
            Content = pp.Content,
            FirstName = pp.FirstName,
            LastName = pp.LastName,
            PostingStartDate = DateTime.Now
        }).ToList();
    }

    public override Task<AzureSearchResult<ParentPageIndexModel>> Search(ParentPageQueryModel query, CancellationToken cancellationToken = default)
    {
        return memoryCache.GetOrCreateAsync(
            (query.SearchString, string.Join("|", query.Categories), string.Join("|", query.Fields), string.Join("|", query.Localities), query.Take, query.Skip, query.Language)
            , entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                if (entry.Key is (string queryText, string categories, string fields, string localities, int take, int skip, string language))
                {
                    query.SearchString = ApplyFuzzySearch(query.SearchString);
                    return SearchInternal(query);
                }
                else
                {
                    return Task.FromResult(new AzureSearchResult<ParentPageIndexModel>
                    {
                        Results = Array.Empty<ParentPageIndexModel>()
                    });
                }
            });
    }

    public async Task<bool> AddToIndexByIDs(ICollection<int> ids)
    {
        if (ids is null || ids.Count == 0)
        {
            return true;
        }
        var jobRequisitions = await parentPageService.GetIndexModelsAsync(ids.ToList());
        return jobRequisitions is null || jobRequisitions.Count == 0 || await AddToIndex(ConvertToIndexModels(jobRequisitions));
    }

    public async Task<bool> RemoveFromIndex(ICollection<int> ids)
    {
        if (ids is null || !ids.Any())
            return true;
        var indexModelsToRemove = new List<ParentPageIndexModel>();
        foreach (var id in ids)
        {
            indexModelsToRemove.Add(new ParentPageIndexModel() { Id = id.ToString() });
        }
        return await RemoveFromIndex(indexModelsToRemove);
    }

    protected override string GetFilter(ParentPageQueryModel typedQuery, string? exceptFilter = null)
    {
        var builder = new SearchFilterBuilder()
            .AddAnyFilter(nameof(ParentPageIndexModel.Tags), typedQuery.Localities, nameof(ParentPageIndexModel.Tags) == exceptFilter);
        return builder.Build();
    }

    protected override void CustomizeIndex(SearchIndex definition)
    {
        var stopwordsTokenFilter = new StopwordsTokenFilter("cs_jim_stopwords")
        {
            StopwordsList = StopwordsList.Czech,
            IgnoreCase = true
        };

        var tokenizer = new MicrosoftLanguageStemmingTokenizer("cs_jim_MicrosoftLanguageStemmingTokenizer")
        {
            Language = MicrosoftStemmingTokenizerLanguage.Czech
        };

        var customAnalyzer = new CustomAnalyzer(AnalyzerName, tokenizer.Name);
        customAnalyzer.TokenFilters.Add(TokenFilterName.Lowercase);
        customAnalyzer.TokenFilters.Add(TokenFilterName.AsciiFolding);
        customAnalyzer.TokenFilters.Add(stopwordsTokenFilter.Name);
        definition.Analyzers.Add(customAnalyzer);
        definition.Tokenizers.Add(tokenizer);
        definition.TokenFilters.Add(stopwordsTokenFilter);

        foreach (var field in definition.Fields.Where(f => f.IsSearchable ?? false))
        {
            field.AnalyzerName = AnalyzerName;
        }
    }

    private static string ApplyFuzzySearch(string searchText)
    {
        return string.Join(" ", searchText.Split(" ").Select(word => string.IsNullOrEmpty(word) ? "" : $"{word}~"));
    }



}
