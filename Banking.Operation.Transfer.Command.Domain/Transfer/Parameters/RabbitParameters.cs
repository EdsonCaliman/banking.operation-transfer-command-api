using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Parameters
{
    [ExcludeFromCodeCoverage]
    public class RabbitParameters
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Queue { get; set; }
    }
}
