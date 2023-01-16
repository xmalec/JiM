using BL.Models.ParentPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.ParentPage
{
    public interface IParentPageService : IService
    {
        IList<ParentPageIndexModelDto> GetAllIndexModelsAsync();
        Task<IList<ParentPageIndexModelDto>> GetIndexModelsAsync(IList<int> ids);
    }
}
