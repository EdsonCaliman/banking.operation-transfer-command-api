using Banking.Operation.Transfer.Command.Domain.Transfer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banking.Operation.Transfer.Command.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<TransferEntity> Transfer { get; set; }
    }
}
