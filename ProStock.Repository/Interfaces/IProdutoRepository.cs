using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IProdutoRepository : IProStockRepository
    {
         Task<Produto[]> GetAllProdutosAsyncByName(string nome);
         Task<Produto[]> GetAllProdutosAsync();
         Task<Produto> GetProdutosAsyncById(int produtoId, bool activatedObjects = true);
    }
}