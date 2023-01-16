using BL.Models.ParentPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.ParentPage
{
    public class ParentPageService : IParentPageService
    {
        public IList<ParentPageIndexModelDto> GetAllIndexModelsAsync()
        {
            return new List<ParentPageIndexModelDto>()
            {
                new ParentPageIndexModelDto()
                {
                    Id= 1,
                    FirstName = "Štefan",
                    LastName = "Loudil",
                    Content = "Content page",
                    Title = "Nabídka práce"
                },
                new ParentPageIndexModelDto()
                {
                    Id= 2,
                    FirstName = "Alexandra",
                    LastName = "Nízkotučná",
                    Content = "Content page 2",
                    Title = "Služby vaření"
                }
            };
        }

        public Task<IList<ParentPageIndexModelDto>> GetIndexModelsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
