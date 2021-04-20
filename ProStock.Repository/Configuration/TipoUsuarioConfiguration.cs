using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class TipoUsuarioConfiguration : IEntityTypeConfiguration<TipoUsuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TipoUsuario> builder)
        {
            builder.ToTable("TipoUsuario");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Descricao).HasMaxLength(200);


            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}