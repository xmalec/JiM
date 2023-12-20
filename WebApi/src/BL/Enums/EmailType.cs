namespace BL.Enums
{
    public enum EmailType
    {
        NewUser
    }

    public static class EmailTypeExtensions
    {
        public static string GetTemplateName(this EmailType emailType)
        {
            return $"{emailType}.cshtml";
        }
    }
    
}
