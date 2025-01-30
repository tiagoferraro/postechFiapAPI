using Microsoft.EntityFrameworkCore;
using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Context;

public class AppDBContext : DbContext
{
    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { 
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }


    public DbSet<DDD> DDD { get; set; }
    public DbSet<Contato> Contatos { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataInclusao") != null))
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataInclusao").IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

}

