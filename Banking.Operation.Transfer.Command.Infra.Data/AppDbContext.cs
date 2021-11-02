using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Banking.Operation.Transfer.Command.Infra.Data
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<TransferEntity> Transfer { get; set; }
    }
}
