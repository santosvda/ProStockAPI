using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository.Configuration
{
    public class ProdutoVendaConfiguration : IEntityTypeConfiguration<ProdutoVenda>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProdutoVenda> builder)
        {
            builder.ToTable("ProdutosVendas");
            builder.HasKey(p => p.Id);
        }
    }
}