namespace BL.Options
{
    public class EmailSettingOptions
    {
        public const string SectionName = "EmailSetting";

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
