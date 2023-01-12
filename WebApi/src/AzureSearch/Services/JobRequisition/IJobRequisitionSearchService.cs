using System.Collections.Generic;
using System.Threading.Tasks;
using AzureSearch.Models;

namespace AzureSearch.Services.JobRequisition;
public interface IJobRequisitionSearchService : IAzureSearchService<JobRequisitionIndexModel, JobRequisitionsQueryModel>
{
    Task<bool> AddToIndexByIDs(ICollection<int> ids);
    Task<bool> RemoveFromIndex(ICollection<int> ids);
}
