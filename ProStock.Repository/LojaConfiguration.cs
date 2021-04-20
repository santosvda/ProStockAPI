using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class LojaConfiguration : IEntityTypeConfiguration<Loja>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Loja> builder)
        {
            builder.ToTable("Loja");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Descricao).HasMaxLength(70);
            builder.Property(p => p.Email).HasMaxLength(50);
            builder.Property(p => p.Telefone).HasMaxLength(14);


            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}