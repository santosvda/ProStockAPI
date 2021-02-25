using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IClienteRepository : IProStockRepository
    {
         //Cliente
         Task<Cliente[]> GetAllClienteAsync();
         Task<Cliente> GetClienteAsyncById(int clienteId);
         Task<Cliente> GetClienteAsyncByCpf(string cpf);
         Task<Cliente[]> GetAllClienteAsyncByName(string nome);

    }
}