using Banking.Operation.Transfer.Command.Domain.Transfer.Enums;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    public class TransactionDto
    {
        public TransactionDto(TransactionType type, decimal value)
        {
            Type = type.ToString();
            Value = value;
        }

        public TransactionDto()
        {
        }

        public string Type { get; set; }
        public decimal Value { get; set; }
    }
}
