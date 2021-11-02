using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    [ExcludeFromCodeCoverage]
    public class ResponseTransferDto
    {
        public ResponseTransferDto(TransferEntity entity)
        {
            Id = entity.Id;
            ContactId = entity.ContactId;
            Value = entity.Value;
            CreatedAt = entity.CreatedAt;
            CreatedBy = entity.CreatedBy;
        }

        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
