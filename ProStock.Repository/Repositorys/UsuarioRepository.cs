using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ProStockContext _context;
        public UsuarioRepository(ProStockContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
         //Gerais
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<Usuario[]> GetAllUsuarioAsync(){
            IQueryable<Usuario> query = _context.Usuarios
            .Include(p => p.Pessoa);

            query = query.AsNoTracking().OrderBy(u => u.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Usuario> GetUsuarioAsyncById (int usuarioId){
            IQueryable<Usuario> query = _context.Usuarios
            .Include(p => p.Pessoa);

            query = query.AsNoTracking().OrderByDescending(u => u.Id)
            .Where(u => u.Id == usuarioId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario[]> GetAllUsuarioAsyncByLogin(string usuarioLogin){
            IQueryable<Usuario> query = _context.Usuarios
            .Include(p => p.Pessoa);
            query = query.AsNoTracking().OrderBy(u => u.Id)
            .Where(u => u.Login == usuarioLogin);

            return await query.ToArrayAsync();
        }

        public async Task<Usuario> Login(Usuario usuario){
            IQueryable<Usuario> query = _context.Usuarios
            .Include(p => p.Pessoa);

            query = query.AsNoTracking().OrderBy(u => u.Id)
            .Where(u => u.Login == usuario.Login && u.Senha == usuario.Senha);

            return await query.FirstOrDefaultAsync();
        }
    }
}