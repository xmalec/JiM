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
            var res = new List<ParentPageIndexModelDto>();
            for(var i = 0; i< 1000; i++)
            {
                res.Add(
                    new ParentPageIndexModelDto()
                    {
                        Id = i,
                        FirstName = "Štefan",
                        LastName = "Loudil",
                        Content = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas lorem. Duis risus. Etiam dictum tincidunt diam. Mauris tincidunt sem sed arcu. Morbi scelerisque luctus velit. Mauris elementum mauris vitae tortor. Duis ante orci, molestie vitae vehicula venenatis, tincidunt ac pede. Donec quis nibh at felis congue commodo. Praesent in mauris eu tortor porttitor accumsan. Aliquam ornare wisi eu metus. Nulla turpis magna, cursus sit amet, suscipit a, interdum id, felis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Etiam bibendum elit eget erat. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Nulla quis diam. In dapibus augue non sapien.\r\n\r\nPellentesque ipsum. Vivamus ac leo pretium faucibus. Pellentesque pretium lectus id turpis. Nulla turpis magna, cursus sit amet, suscipit a, interdum id, felis. Duis risus. Vestibulum erat nulla, ullamcorper nec, rutrum non, nonummy ac, erat. Fusce aliquam vestibulum ipsum. Maecenas ipsum velit, consectetuer eu lobortis ut, dictum at dui. Nullam lectus justo, vulputate eget mollis sed, tempor sed magna. Nulla accumsan, elit sit amet varius semper, nulla mauris mollis quam, tempor suscipit diam nulla vel leo. Aliquam ante. Nam sed tellus id magna elementum tincidunt. Morbi leo mi, nonummy eget tristique non, rhoncus non leo. Vivamus ac leo pretium faucibus. Quisque tincidunt scelerisque libero. Sed ac dolor sit amet purus malesuada congue. Duis sapien nunc, commodo et, interdum suscipit, sollicitudin et, dolor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.",
                        Title = "Nabídka práce"
                    }
                );
            }
            return res;
        }

        public Task<IList<ParentPageIndexModelDto>> GetIndexModelsAsync(IList<int> ids)
        {
            throw new NotImplementedException();
        }
    }
}
