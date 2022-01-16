using Banking.Operation.Transfer.Command.Domain.Transfer.Dtos;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public interface IBalanceService
    {
        Task<BalanceDto> GetBalance(Guid clientId);
    }
}
