using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public interface ITransferService
    {
        Task<ResponseTransferDto> Save(Guid clientId, RequestTransferDto transfer);
    }
}
