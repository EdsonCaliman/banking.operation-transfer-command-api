using Microsoft.EntityFrameworkCore;

namespace Banking.Operation.Transfer.Command.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}
