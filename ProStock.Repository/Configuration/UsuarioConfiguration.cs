using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Domain.Enums;

namespace ProStock.Repository.Configuration
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Senha).HasMaxLength(100);
            builder.Property(p => p.Login).HasMaxLength(20);

            builder.Property(p => p.TipoUsuario).IsRequired(true);
            builder.Property(p => p.TipoUsuario).HasDefaultValue(TipoUsuario.Operador);


            builder.Property(p => p.Ativo).HasDefaultValue(true);
            builder.HasIndex(p => p.Ativo).IsUnique(false);
        }
    }
}