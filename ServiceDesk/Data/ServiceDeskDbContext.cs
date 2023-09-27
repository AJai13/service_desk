using Microsoft.EntityFrameworkCore;
using ServiceDesk;

namespace ServiceDesk.Data;
public class ServiceDeskDbContext : DbContext
{
    public DbSet<Dispositivo>? Dispositivo { get; set; }
    public DbSet<Funcionario>? Funcionario { get; set; }
    public DbSet<Usuario>? Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=servicedesk.db;Cache=Shared");
    }
}