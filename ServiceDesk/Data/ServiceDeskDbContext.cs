using Microsoft.EntityFrameworkCore;
using ServiceDesk;
using ServiceDesk.Models;

namespace ServiceDesk.Data;
public class ServiceDeskDbContext : DbContext
{
    public DbSet<Dispositivo>? Dispositivo { get; set; }
    public DbSet<Funcionario>? Funcionario { get; set; }
    public DbSet<Usuario>? Usuario { get; set; }
    public DbSet<Categoria>? Categoria { get; set; }
    public DbSet<Filtro> Filtro { get; set; }
    public DbSet<Prioridade> Prioridade { get; set; }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<Solucao>? Solucao { get; set; }
    public DbSet<Feedback>? Feedback { get; set; }
    public DbSet<Status>? Status { get; set; }
    public DbSet<Sla>? Sla { get; set; }
    public DbSet<CentroDeCusto> CentroDeCusto {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("DataSource=servicedesk.db;Cache=Shared");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Dispositivo)
            .WithOne(d => d.Usuario)
            .HasForeignKey<Dispositivo>(d => d.UsuarioId) // Chave estrangeira em Dispositivo
            .OnDelete(DeleteBehavior.SetNull); // Isso permite que o UsuarioId seja nulo

        modelBuilder.Entity<CentroDeCusto>()
            .HasOne(c => c.Usuario)
            .WithOne(u => u.CentroDeCusto)
            .HasForeignKey<Usuario>(u => u.CentroDeCustoId);
    }






}