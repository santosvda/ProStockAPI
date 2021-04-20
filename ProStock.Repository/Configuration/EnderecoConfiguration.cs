using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("Endereco");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Bairro).HasMaxLength(50);
            builder.Property(p => p.Cep).HasMaxLength(9);
            builder.Property(p => p.Cidade).HasMaxLength(50);
            builder.Property(p => p.Complemento).HasMaxLength(70);
            builder.Property(p => p.Pais).HasMaxLength(50);
            builder.Property(p => p.Rua).HasMaxLength(50);
            builder.Property(p => p.Uf).HasMaxLength(2);
            builder.Property(p => p.Numero).HasDefaultValue(0);


            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}