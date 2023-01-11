using BL.Models.Email;
using BL.Services.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PlaygroundController : ApiControllerBase
    {
        private readonly IEmailService emailService;

        public PlaygroundController(IEmailService emailService)
        {
            this.emailService = emailService;
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
