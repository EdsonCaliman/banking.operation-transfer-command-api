using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using Banking.Operation.Transfer.Command.Domain.Transfer.Parameters;
using Banking.Operation.Transfer.Command.Domain.Transfer.Services;
using Confluent.Kafka;
using System.Text.Json;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Infra.Data.MessageBroker
{
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
