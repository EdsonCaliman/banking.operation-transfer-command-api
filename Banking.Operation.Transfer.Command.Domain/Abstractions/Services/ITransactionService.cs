using Banking.Operation.Transfer.Command.Domain.Transfer.Enums;
using System;
using System.Threading.Tasks;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Services
{
    public interface ITransactionService
    {
        Task Post(Guid clientId, TransactionType type, decimal value);
    }
}
