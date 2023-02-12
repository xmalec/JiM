using BL.Enums;
using BL.Models.Email;
using BL.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorEngineCore;
using System.Text;

namespace BL.Services.Email
{
    public class EmailService : IEmailService
    {

        private readonly EmailSettingOptions emailSetting;

        public EmailService(IOptions<EmailSettingOptions> emailSettingOptions)
        {
            emailSetting = emailSettingOptions.Value;
        }

        public async Task SendEmailAsync(EmailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(emailSetting.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = BuildEmailBody(mailRequest.EmailType, mailRequest.EmailBodyModel);
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress("LabNet", emailSetting.DisplayName));
            using var smtp = new SmtpClient();
            smtp.Connect(emailSetting.Host, emailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailSetting.Email, emailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string BuildEmailBody(EmailType emailType, EmailBodyModel emailTemplateModel)
        {
            return FillTemplate(LayoutModel.TemplateName, new LayoutModel()
            {
                Content = FillTemplate(emailType.GetTemplateName(), emailTemplateModel)
            });
        }

        private string FillTemplate(string templateName, EmailBodyModel emailTemplateModel) {
            string mailTemplate = LoadTemplate(templateName);

            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);

            return modifiedMailTemplate.Run(emailTemplateModel);
        }

        private string LoadTemplate(string emailTemplate)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string templateDir = Path.Combine(baseDir, "Templates");
            string templatePath = Path.Combine(templateDir, emailTemplate);
            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}
