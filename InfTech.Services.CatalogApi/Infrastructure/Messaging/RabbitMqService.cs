using InfTech.Services.CatalogApi.Domain.Messaging;
using RabbitMQ.Client;
using System.Text;

namespace InfTech.Services.CatalogApi.Infrastructure.Messaging
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMqService() 
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public void SendMessage(RmqMessage message)
        {
            _channel.QueueDeclare(
                queue: message.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var body = Encoding.UTF8.GetBytes(message.Message);

                _channel.BasicPublish(
                    exchange: "",
                    routingKey: message.QueueName,
                    basicProperties: null,
                    body: body);
        }
        public void Dispose()
        {
            _connection.Dispose();
            _channel.Dispose();
        }
    }
}
