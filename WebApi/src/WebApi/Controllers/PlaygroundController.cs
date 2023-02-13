using BL.Constants;
using BL.Models.Email;
using BL.Models.File;
using BL.Services.Email;
using BL.Services.File;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PlaygroundController : ApiControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IFileService fileService;

        public PlaygroundController(IEmailService emailService, IFileService fileService)
        {
            this.emailService = emailService;
            this.fileService = fileService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SaveFile([FromBody] FileModel fileModel)
        {
            //fileModel.Binary = System.IO.File.ReadAllBytes("../../data/imageDisplay.gif");
            //fileModel.FileType = "image/gif";
            await fileService.SaveFile(fileModel, FileType.ImageUserPage);
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SendEmail()
        {
            var request = new EmailRequest()
            {
                ToEmail = "test@mail.com",
                Subject = "test subject",
                EmailBodyModel = new NewUserModel
                {
                    Email = "newemail@mail.com",
                    Name = "Pan Stonožka"
                },
                EmailType = BL.Enums.EmailType.NewUser
            };
            await emailService.SendEmailAsync(request);
            return Ok();
        }
    }
}
