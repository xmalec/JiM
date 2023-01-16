using System;
using System.Collections.Generic;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace AzureSearch.Models.ParentPage;

public class ParentPageIndexModel : BaseIndexModel
{
    [SearchableField(IsFilterable = true)]
    public string FirstName { get; set; } = "";
    [SearchableField(IsFilterable = true)]
    public string LastName { get; set; } = "";
    [SearchableField(IsFilterable = true)]
    public string Title { get; set; } = "";
    [SearchableField(IsFilterable = true)]
    public string Content { get; set; } = "";

    [SimpleField(IsFilterable = true, IsFacetable = true)]
    public IReadOnlyList<string> Tags { get; set; } = new List<string>();
    [SimpleField(IsSortable = true)]
    public DateTimeOffset PostingStartDate { get; set; }
}
