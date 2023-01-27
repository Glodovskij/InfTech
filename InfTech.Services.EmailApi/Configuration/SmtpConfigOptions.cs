namespace InfTech.Services.EmailApi.Configuration
{
    public class SmtpConfigOptions
    {
        public const string SmtpConfiguration = "SmtpConfiguration";
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Host { get; set; } = String.Empty;
        public int Port { get; set; }
    }
}
