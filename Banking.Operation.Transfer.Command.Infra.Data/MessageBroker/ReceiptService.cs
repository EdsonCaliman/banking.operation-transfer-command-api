using Banking.Operation.Transfer.Command.Domain.Abstractions.Services;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Confluent.Kafka;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Infra.Data.MessageBroker
{
    [ExcludeFromCodeCoverage]
    public class ReceiptService : IReceiptService
    {
        private readonly KafkaParameters _kafkaParameters;

        public ReceiptService(KafkaParameters kafkaParameters)
        {
            _kafkaParameters = kafkaParameters;
        }

        public async Task PublishReceipt(ReceiptDto receipt)
        {
            var config = new ProducerConfig { BootstrapServers = _kafkaParameters.BootstrapServers };

            using var p = new ProducerBuilder<Null, string>(config).Build();

            string jsonString = JsonSerializer.Serialize(receipt);

            await p.ProduceAsync(_kafkaParameters.Topic, new Message<Null, string> { Value = jsonString });
        }
    }
}
