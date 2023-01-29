namespace InfTech.Services.CatalogApi.Domain.Messaging
{
    public class RmqMessage
    {
        public string QueueName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
