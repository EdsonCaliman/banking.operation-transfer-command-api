using Banking.Operation.Transfer.Command.Domain.Abstractions.Helpers;
using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Entities
{
    public class TransferEntity
    {
        public TransferEntity(ClientDto client, ContactDto contact, decimal value)
        {
            ClientId = client.Id;
            ContactId = contact.Id;
            Value = value;
            CreatedAt = DateTime.Now;
            CreatedBy = CreatorHelper.GetEntityCreatorIdentity();
        }

        public TransferEntity()
        {
        }

        [Key]
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid ContactId { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
