using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface ITipoUsuarioRepository : IProStockRepository
    {
         Task<TipoUsuario[]> GetAllTipoUsuarioAsyncByDescricao(string descricao);
         Task<TipoUsuario[]> GetAllTipoUsuarioAsync();
         Task<TipoUsuario> GetTipoUsuarioAsyncById(int tipoId);
    }
}