using Banking.Operation.Transfer.Command.Domain.Abstractions.Services;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Banking.Operation.Transfer.Command.Infra.Data.MessageBroker
{
    public class NotificationService : INotificationService
    {
        private readonly RabbitParameters _rabbitParameters;

        public NotificationService(RabbitParameters rabbitParameters)
        {
            _rabbitParameters = rabbitParameters;
        }

        public void PublishMessage(MessageDto message)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = _rabbitParameters.HostName, 
                UserName = _rabbitParameters.UserName, 
                Password = _rabbitParameters.Password, 
                Port = _rabbitParameters.Port
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: _rabbitParameters.Queue, 
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            string jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(exchange: string.Empty,                
                                 routingKey: _rabbitParameters.Queue,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
