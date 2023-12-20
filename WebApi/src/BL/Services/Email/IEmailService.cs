using BL.Models.Email;

namespace BL.Services.Email
{
    public interface IEmailService : IService
    {
        Task SendEmailAsync(EmailRequest mailRequest);
        Task SendNewUserEmail(NewUserModel model, bool awaitEmailSend = false);
    }
}
