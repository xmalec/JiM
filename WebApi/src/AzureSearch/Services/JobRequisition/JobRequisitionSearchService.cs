﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using AzureSearch.Models;
using BL.Models.ParentPage;
using BL.Services.ParentPage;
using Bluesoft.Utils.AzureSearchFilterBuilder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace AzureSearch.Services.JobRequisition;
public class JobRequisitionSearchService : AzureSearchService<JobRequisitionIndexModel, JobRequisitionsQueryModel>, IJobRequisitionSearchService
{
    private readonly IMemoryCache memoryCache;
    private readonly IParentPageService parentPageService;

    public const string AnalyzerName = "jobRequisitionAnalyzer";
    public JobRequisitionSearchService(
            IAzureClientFactory<SearchClient> clientFactory,
            ILogger<JobRequisitionSearchService> logger,
            SearchIndexClient indexClient,
            IMemoryCache memoryCache
,
            IParentPageService parentPageService)
        : base(logger, indexClient, clientFactory.CreateClient("JobSearchClient"))
    {
        this.memoryCache = memoryCache;
        this.parentPageService = parentPageService;
    }

    protected override async Task<bool> IndexAll()
    {
        var jobRequisitions = await parentPageService.GetAllIndexModelsAsync();
        return await AddToIndex(ConvertToIndexModels(jobRequisitions));
    }

    private IList<JobRequisitionIndexModel> ConvertToIndexModels(IList<ParentPageIndexModelDto> jobRequisitions)
    {
        return jobRequisitions.Select(jr => new JobRequisitionIndexModel()
        {
        }).ToList();
    }

    public override Task<AzureSearchResult<JobRequisitionIndexModel>> Search(JobRequisitionsQueryModel query, CancellationToken cancellationToken = default)
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
                    return Task.FromResult(new AzureSearchResult<JobRequisitionIndexModel>
                    {
                        Results = Array.Empty<JobRequisitionIndexModel>()
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
        var indexModelsToRemove = new List<JobRequisitionIndexModel>();
        foreach (var id in ids)
        {
            indexModelsToRemove.Add(new JobRequisitionIndexModel() { Id = id.ToString() });
        }
        return await RemoveFromIndex(indexModelsToRemove);
    }

    protected override string GetFilter(JobRequisitionsQueryModel typedQuery, string? exceptFilter = null)
    {
        var builder = new SearchFilterBuilder()
            .AddFilter($"{nameof(JobRequisitionIndexModel.Secret)} eq null")
            .AddEqualsFilter(nameof(JobRequisitionIndexModel.Language), typedQuery.Language)
            .AddInFilter(nameof(JobRequisitionIndexModel.CategorySlug), typedQuery.Categories, nameof(JobRequisitionIndexModel.CategorySlug) == exceptFilter)
            .AddAnyFilter(nameof(JobRequisitionIndexModel.FieldSlugs), typedQuery.Fields, nameof(JobRequisitionIndexModel.FieldSlugs) == exceptFilter)
            .AddAnyFilter(nameof(JobRequisitionIndexModel.LocationSlugs), typedQuery.Localities, nameof(JobRequisitionIndexModel.LocationSlugs) == exceptFilter);
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
