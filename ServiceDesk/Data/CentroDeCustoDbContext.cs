using Microsoft.EntityFrameworkCore;
using ServiceDesk;

public class CentroDeCustoDbContext : DbContext
{
    public DbSet<CentroDeCusto>? CentroDeCusto { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=servicedesk.db;Cache=Shared");
    }
}