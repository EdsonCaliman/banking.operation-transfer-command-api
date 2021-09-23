using Banking.Operation.Transfer.Command.Domain.Abstractions.Repositories;
using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;

namespace Banking.Operation.Transfer.Command.Domain.Transfer.Repositories
{
    public interface ITransferRepository : IBaseRepository<TransferEntity>
    {
    }
}
