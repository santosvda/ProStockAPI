using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        private readonly ProStockContext _context;
        public TipoUsuarioRepository(ProStockContext context)
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

        public async Task<TipoUsuario[]> GetAllTipoUsuarioAsync(){
            IQueryable<TipoUsuario> query = _context.TiposUsuarios;

            query = query.AsNoTracking().OrderBy(tp => tp.Id);

            return await query.ToArrayAsync();
        }

        public async Task<TipoUsuario[]> GetAllTipoUsuarioAsyncByDescricao (string descricao){
            IQueryable<TipoUsuario> query = _context.TiposUsuarios;

            query = query.AsNoTracking().OrderByDescending(tp => tp.Descricao)
            .Where(tp => tp.Descricao.ToLower().Contains(descricao.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<TipoUsuario> GetTipoUsuarioAsyncById (int tipoId){
            IQueryable<TipoUsuario> query = _context.TiposUsuarios;

            query = query.AsNoTracking().OrderByDescending(tp => tp.Descricao)
            .Where(tp => tp.Id == tipoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}