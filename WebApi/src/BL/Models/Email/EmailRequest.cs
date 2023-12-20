using BL.Enums;

namespace BL.Models.Email
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public EmailType EmailType { get; set; }
        public EmailBodyModel EmailBodyModel { get; set; }
    }
}
