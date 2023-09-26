using Microsoft.EntityFrameworkCore;
using ServiceDesk;

public class UsuarioDbContext : DbContext
{
    public DbSet<Usuario>? Usuario { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=servicedesk.db;Cache=Shared");
    }
}