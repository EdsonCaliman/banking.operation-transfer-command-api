using Banking.Operation.Transfer.Command.Domain.Abstractions.Services;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Banking.Operation.Transfer.Command.Infra.Data.MessageBroker
{
    public class NotificationService : INotificationService
    {
        public void PublishMessage(MessageDto message)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost", 
                UserName = "admin", 
                Password = "admin", 
                Port = 5672, 
                VirtualHost = "test" 
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

            string jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(exchange: "logs",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
        }
    }
}
