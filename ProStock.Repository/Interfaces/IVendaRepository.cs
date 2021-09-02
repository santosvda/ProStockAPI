using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IVendaRepository : IProStockRepository
    {
        Task<Venda[]> GetAllVendasAsyncByUserId(int VendaId);
        Task<Venda[]> GetAllVendasAsyncByClientId(int VendaId);
        Task<Venda[]> GetAllVendasAsync();
        Task<Venda> GetVendasAsyncById(int VendaId);
    }
}