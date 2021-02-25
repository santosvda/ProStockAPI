using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IPessoaRepository : IProStockRepository
    {
         Task<Pessoa[]> GetAllPessoaAsyncByName(string nome);
         Task<Pessoa[]> GetAllPessoaAsync();
         Task<Pessoa> GetPessoaAsyncById(int pessoaId);
         Task<Pessoa> GetPessoaAsyncByCpf(string cpf);
    }
}