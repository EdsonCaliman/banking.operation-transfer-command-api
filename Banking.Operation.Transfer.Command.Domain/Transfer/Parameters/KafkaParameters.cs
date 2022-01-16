using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Parameters
{
    [ExcludeFromCodeCoverage]
    public class KafkaParameters
    {
        public string BootstrapServers { get; set; }
        public string Topic { get; set; }
    }
}
