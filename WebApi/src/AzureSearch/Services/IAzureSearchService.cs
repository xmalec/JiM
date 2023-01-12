using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AzureSearch.Models;

namespace AzureSearch.Services;
public interface IAzureSearchService<TResult, TQuery>
    where TResult : BaseIndexModel
    where TQuery : QueryBaseModel
{
    Task<bool> RebuildIndex();
    Task<AzureSearchResult<TResult>> Search(TQuery query, CancellationToken cancellationToken = default);
    Task<bool> RemoveFromIndex(IList<TResult> toRemove);
}
