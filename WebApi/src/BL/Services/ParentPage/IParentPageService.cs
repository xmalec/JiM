using BL.Models.ParentPage;

namespace BL.Services.ParentPage
{
    public interface IParentPageService : IService
    {
        IList<ParentPageIndexModelDto> GetAllIndexModelsAsync();
        Task<IList<ParentPageIndexModelDto>> GetIndexModelsAsync(IList<int> ids);
    }
}
