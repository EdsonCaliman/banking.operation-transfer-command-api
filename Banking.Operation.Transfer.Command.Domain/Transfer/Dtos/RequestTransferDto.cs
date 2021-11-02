using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Dtos
{
    [ExcludeFromCodeCoverage]
    public class RequestTransferDto
    {
        [Required(ErrorMessage = "ContactId is mandatory")]
        public Guid ContactId { get; set; }
        [Required(ErrorMessage = "Value is mandatory")]
        [Range(0.1, 10000, ErrorMessage = "Value must be between 0.1 and 10000")]
        public decimal Value { get; set; }
    }
}
