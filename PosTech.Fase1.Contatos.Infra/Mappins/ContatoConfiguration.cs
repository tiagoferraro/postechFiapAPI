using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PosTech.Fase1.Contatos.Domain.Entities;

namespace PosTech.Fase1.Contatos.Infra.Mappins
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.ToTable("Contato");
            builder.HasKey(c => c.ContatoId);
            builder.Property(c => c.ContatoId).IsRequired();
            builder.Property(c => c.Nome).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Telefone).HasMaxLength(15).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(200);
            builder.Property(c => c.DddId).IsRequired();
            builder.HasOne(c => c.Ddd).WithMany().HasForeignKey(c => c.DddId);
            builder.Property(c => c.Ativo).IsRequired();
            builder.Property(c => c.DataInclusao).HasColumnType("smalldatetime");
            
        }
    }
}
