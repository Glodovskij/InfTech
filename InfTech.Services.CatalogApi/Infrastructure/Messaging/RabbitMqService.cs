using InfTech.Services.CatalogApi.Domain.Messaging;
using RabbitMQ.Client;
using System.Text;

namespace InfTech.Services.CatalogApi.Infrastructure.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(string message)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: "InfTech-email",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: "InfTech-email",
                    basicProperties: null,
                    body: body);
            }
        }
    }
}
