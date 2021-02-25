using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface ILojaRepository : IProStockRepository
    {
         Task<Loja[]> GetAllLojaAsync();
         Task<Loja> GetLojaAsyncById(int clienteId);
         Task<Loja[]> GetAllLojaAsyncByDescricao(string descricao);
    }
}