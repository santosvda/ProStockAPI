using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Configuration;

namespace ProStock.Repository
{
    public class ProStockContext : DbContext
    {
        public ProStockContext(DbContextOptions<ProStockContext> options) : base (options){ }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Loja> Lojas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoVenda> ProdutosVendas { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
            modelBuilder.ApplyConfiguration(new EstoqueConfiguration());
            modelBuilder.ApplyConfiguration(new LojaConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new VendaConfiguration());
            /*
                sobrescrevendo o metodo
                especificando a relação (n,n) Produto - Venda 
            */
            modelBuilder.Entity<ProdutoVenda>()
            .HasKey(PV => new { PV.ProdutoId, PV.VendaId });
        }
    }
}