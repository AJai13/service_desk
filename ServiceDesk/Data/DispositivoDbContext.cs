using Microsoft.EntityFrameworkCore;
using ServiceDesk;

namespace ServiceDesk.Data;
public class DispositivoDbContext : DbContext
{
    public DbSet<Dispositivo>? Dispositivo { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=servicedesk.db;Cache=Shared");
    }
}