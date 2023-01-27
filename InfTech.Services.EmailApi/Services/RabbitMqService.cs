using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace InfTech.Services.EmailApi.Services
{
    public class RabbitMqService : BackgroundService
    {
        private readonly ISmtpClient _smtpClient;
        private readonly IModel _channel;
        private readonly IConnection _connection;
        public RabbitMqService(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                    queue: "InfTech-email",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                _smtpClient.Send(new List<string>() { "glodovskii@gmail.com" }, "New Product Added", message);
            };

            _channel.BasicConsume(
                queue: "InfTech-email",
                autoAck: true,
                consumer: consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
            base.Dispose();
        }
    }
}
