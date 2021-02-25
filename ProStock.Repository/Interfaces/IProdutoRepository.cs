using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IProdutoRepository : IProStockRepository
    {
         Task<Produto[]> GetAllProdutosAsyncByName(string nome, bool includeVendas);
         Task<Produto[]> GetAllProdutosAsync(bool includeVendas);
         Task<Produto> GetProdutosAsyncById(int produtoId, bool includeVendas);
    }
}