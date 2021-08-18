using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Vendas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Status).HasMaxLength(50);
            builder.Property(p => p.Descricao).HasMaxLength(50);

            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}