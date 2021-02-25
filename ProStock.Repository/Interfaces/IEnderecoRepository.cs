using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IEnderecoRepository : IProStockRepository
    {
         Task<Endereco> GetEnderecoAsyncById(int enderecoId);
         Task<Endereco[]> GetAllEnderecoAsyncByCep(string cep);
         Task<Endereco[]> GetAllEnderecoAsync();
         Task<Endereco[]> GetAllEnderecoAsyncByCidade(string cidade);
         Task<Endereco[]> GetAllEnderecoAsyncByRua(string rua);
    }
}