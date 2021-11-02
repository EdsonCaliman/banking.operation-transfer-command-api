using System;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    [ExcludeFromCodeCoverage]
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
    }
}
