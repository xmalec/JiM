using System.Collections.Generic;
using System.Threading.Tasks;
using AzureSearch.Models.ParentPage;

namespace AzureSearch.Services.ParentPage;
public interface IParentPageSearchService : IAzureSearchService<ParentPageIndexModel, ParentPageQueryModel>
{
    Task<bool> AddToIndexByIDs(ICollection<int> ids);
    Task<bool> RemoveFromIndex(ICollection<int> ids);
}
