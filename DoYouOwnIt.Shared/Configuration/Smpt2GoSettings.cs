namespace DoYouOwnIt.Shared.Configuration
{
    public class Smtp2GoSettings
    {
        public string SmtpServer { get; set; } = "mail.smtp2go.com";
        public int SmtpPort { get; set; } = 2525;
        public string Username { get; set; } = string.Empty;
        public string Password {  get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
    }
}
