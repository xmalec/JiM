using System;
using Azure;

namespace AzureSearch;
public class SearchClientOptions
{
    public const string SectionName = "AzureSearchClient";
    public string? CredentialKey { get; set; }
    public string? IndexNameBlogPost { get; set; }
    public string? IndexNameJobRequisition { get; set; }
    public Uri? Endpoint { get; set; }
}
