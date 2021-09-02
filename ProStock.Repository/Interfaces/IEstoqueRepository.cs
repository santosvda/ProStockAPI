using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IEstoqueRepository : IProStockRepository
    {
         Task<Estoque[]> GetAllEstoqueAsync();
         Task<Estoque> GetEstoqueAsyncByProdutoId(int produtoId);
         Task<Estoque> GetEstoqueAsyncById(int estoqueId);
    }
}