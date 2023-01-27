using InfTech.Services.EmailApi.Configuration;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.Extensions.Options;
using System.Net.Mail;

namespace InfTech.Services.EmailApi.Services
{
    public class SmtpClient : ISmtpClient
    {
        private readonly SmtpConfigOptions _options;
        public SmtpClient(IOptions<SmtpConfigOptions> options)
        {
            _options = options.Value;
        }
        public void Send(List<string> recipients, string subject, string body)
        {
            using (var client = new System.Net.Mail.SmtpClient(_options.Host, _options.Port))
            {
                client.Credentials = new System.Net.NetworkCredential(_options.Username, _options.Password);
                client.EnableSsl = true;
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(_options.Username),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(string.Join(',', recipients.ToArray()));
                client.Send(message);
            }
        }
    }
}
