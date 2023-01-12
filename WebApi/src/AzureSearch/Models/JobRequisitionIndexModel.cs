using System;
using System.Collections.Generic;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace AzureSearch.Models;

public class JobRequisitionIndexModel : BaseIndexModel
{
    [SimpleField(IsFilterable = true)]
    public string SapId { get; set; }

    [SearchableField(IsFilterable = true)]
    public string Title { get; set; } = "";
    [SimpleField(IsFilterable = true)]
    public string? Language { get; set; }
    [SimpleField(IsFilterable = true)]
    public string? Secret { get; set; }
    [SimpleField]
    public string? Url { get; set; }
    [SearchableField]
    public string? Category { get; set; }
    [SimpleField(IsFilterable = true, IsFacetable = true)]
    public string? CategorySlug { get; set; }
    [SimpleField(IsFilterable = true, IsFacetable = true)]
    public IReadOnlyList<string> FieldSlugs { get; set; } = new List<string>();
    [SimpleField(IsFilterable = true, IsFacetable = true)]
    public IReadOnlyList<string> LocationSlugs { get; set; } = new List<string>();
    [SearchableField]
    public IReadOnlyList<string> Locations { get; set; } = new List<string>();
    [SearchableField]
    public IReadOnlyList<string> Fields { get; set; } = new List<string>();
    [SimpleField(IsSortable = true)]
    public DateTimeOffset PostingStartDate { get; set; }
}
