using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class EstoqueConfiguration : IEntityTypeConfiguration<Estoque>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Estoque> builder)
        {
            builder.ToTable("Estoques");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.QtdAtual).HasDefaultValue(0);
            builder.Property(p => p.QtdMinima).HasDefaultValue(0);

            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}