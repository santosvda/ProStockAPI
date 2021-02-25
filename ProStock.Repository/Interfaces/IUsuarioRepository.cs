using System.Threading.Tasks;
using ProStock.Domain;

namespace ProStock.Repository.Interfaces
{
    public interface IUsuarioRepository : IProStockRepository
    {
         Task<Usuario[]> GetAllUsuarioAsync();
         Task<Usuario> GetUsuarioAsyncById(int usuarioId);
         Task<Usuario[]> GetAllUsuarioAsyncByLogin(string login);
         Task<Usuario> Login(Usuario usuario);
    }
}