namespace InfTech.Services.EmailApi.Services
{
    public interface ISmtpClient
    {
        void Send(List<string> recipients, string subject, string body);
    }
}
