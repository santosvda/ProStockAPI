using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository
{
    public interface IProStockRepository
    {
        //Geral
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         void DeleteRange<T>(T[] entity) where T : class;
         Task<bool> SaveChangesAsync();

         //Produto
         Task<Produto[]> GetAllProdutosAsyncByName(string nome, bool includeVendas);
         Task<Produto[]> GetAllProdutosAsync(bool includeVendas);
         Task<Produto> GetAllProdutosAsyncById(int produtoId, bool includeVendas);

         //Produto
         Task<TipoUsuario[]> GetAllTipoUsuarioAsyncByDescricao(string descricao);
         Task<TipoUsuario[]> GetAllTipoUsuarioAsync();
         Task<TipoUsuario> GetAllTipoUsuarioAsyncById(int tipoId);

         //Pessoa
         Task<Pessoa[]> GetAllPessoaAsyncByName(string nome);
         Task<Pessoa[]> GetAllPessoaAsync();
         Task<Pessoa> GetAllPessoaAsyncById(int pessoaId);
         Task<Pessoa> GetAllPessoaAsyncByCpf(string cpf);

         //Endereço
         Task<Endereco[]> GetAllEnderecoAsyncByCep(string cep);
         Task<Endereco[]> GetAllEnderecoAsync();
         Task<Endereco[]> GetAllEnderecoAsyncByCidade(string cidade);
         Task<Endereco[]> GetAllEnderecoAsyncByRua(string rua);
    }
}