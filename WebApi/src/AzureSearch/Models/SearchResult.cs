using System.Collections.Generic;
using System.Linq;

namespace AzureSearch.Models;

public class AzureSearchResult<T>
{
    public long PageCount { get; internal set; }
    public int PageIndex { get; internal set; }
    public int PageSize { get; internal set; }
    public IReadOnlyList<T> Results { get; internal set; }
    public long TotalCount { get; internal set; }
    public ILookup<string, FacetValue> Facets { get; set; }
}
