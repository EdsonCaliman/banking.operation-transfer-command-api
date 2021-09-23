using System;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    public class RequestTransferDto
    {
        public Guid ContactId { get; set; }
        public decimal Value { get; set; }
    }
}
