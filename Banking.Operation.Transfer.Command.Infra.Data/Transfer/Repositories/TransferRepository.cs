using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Banking.Operation.Transfer.Command.Domain.Transfer.Repositories;
using Banking.Operation.Transfer.Command.Infra.Data.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Infra.Data.Transfer.Repositories
{
    [ExcludeFromCodeCoverage]
    public class TransferRepository : BaseRepository<TransferEntity>, ITransferRepository
    {
        public TransferRepository(AppDbContext context)
            : base(context) { }
    }
}
