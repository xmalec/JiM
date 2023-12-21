using BL.Constants;
using BL.Models.Email;
using BL.Models.File;
using BL.Services.Email;
using BL.Services.File;
using Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace WebApi.Controllers
{
    public class PlaygroundController : ApiControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IFileService fileService;
        private readonly ILogger<PlaygroundController> logger;
        private readonly IStringLocalizer<SharedResource> stringLocalizer;

        public PlaygroundController(IEmailService emailService, IFileService fileService, ILogger<PlaygroundController> logger, IStringLocalizer<SharedResource> stringLocalizer)
        {
            this.emailService = emailService;
            this.fileService = fileService;
            this.logger = logger;
            this.stringLocalizer = stringLocalizer;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Log()
        {
            logger.LogInformation("Shouldnt log");
            logger.LogWarning("Should log");
            logger.LogError("Should log Error");
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> LocalizationTest()
        {
            var a = stringLocalizer["Hello"];
            return Ok(a);
        }
    }
}
