namespace BL.Models.Email
{
    public class LayoutModel : EmailBodyModel
    {
        public const string TemplateName = "emailTemplate.cshtml";

        public string Content { get; set; }
        public string HelpEmail { get; set; }
        public string Title { get; set; }
    }
}
