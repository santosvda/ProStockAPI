using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Email).HasMaxLength(50);
            builder.Property(p => p.Nome).HasMaxLength(70);
            builder.Property(p => p.Telefone).HasMaxLength(14);
            builder.Property(p => p.Cpf).HasMaxLength(11);


            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}