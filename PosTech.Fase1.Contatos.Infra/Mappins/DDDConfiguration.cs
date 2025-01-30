using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Mappins;

public class DDDConfiguration : IEntityTypeConfiguration<DDD>
{
    public void Configure(EntityTypeBuilder<DDD> builder)
    {
        builder.ToTable("Ddd");
        builder.HasKey(x => x.DddId);
        builder.Property(x => x.DddId).ValueGeneratedNever();
        builder.Property(x => x.Regiao).HasMaxLength(50).IsRequired();
        builder.OwnsOne(x => x.UnidadeFederativa, Uf =>
        {
            Uf.Property(p => p.Sigla).HasMaxLength(2).HasColumnName("UfSigla").IsRequired();
            Uf.Property(p => p.Nome).HasMaxLength(100).HasColumnName("UfNome").IsRequired();
        });

    
    }
}

