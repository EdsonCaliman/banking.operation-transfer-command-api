using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public interface IContactService
    {
        Task<ContactDto> GetOne(Guid clientid, Guid id);
    }
}
