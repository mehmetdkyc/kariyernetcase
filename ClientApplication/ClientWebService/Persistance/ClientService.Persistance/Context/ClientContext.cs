using ClientService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClientService.Persistance.Context
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> contextOptions) : base(contextOptions)
        {
            AppContext.SetSwitch("Npsql.EnableLegacyTimestampBehavior", true);
        }
        public DbSet<Company> Companies { get; set; }
    }
}
