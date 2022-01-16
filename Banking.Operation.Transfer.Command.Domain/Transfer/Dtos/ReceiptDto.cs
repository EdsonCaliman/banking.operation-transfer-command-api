using System;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    public class ReceiptDto
    {
        public ReceiptDto(Guid id, Guid clientId, string clientName, Guid contactId, 
            string contactName, decimal value, DateTime createdAt, string createdBy)
        {
            Id = id;
            ClientId = clientId;
            ClientName = clientName;
            ContactId = contactId;
            ContactName = contactName;
            Value = value;
            CreatedAt = createdAt;
            CreatedBy = createdBy;
        }

        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid ContactId { get; set; }
        public string ContactName { get; set; }
        public decimal Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
