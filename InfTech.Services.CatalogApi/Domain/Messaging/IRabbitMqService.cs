namespace InfTech.Services.CatalogApi.Domain.Messaging
{
    public interface IRabbitMqService
    {
        public void SendMessage(RmqMessage message);
    }
}
