using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class LojaRepository : ILojaRepository
    {
        private readonly ProStockContext _context;
        public LojaRepository(ProStockContext context)
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

        public async Task<Loja[]> GetAllLojaAsync(){
            IQueryable<Loja> query = _context.Lojas
            .Include(l => l.Endereco);

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Loja> GetLojaAsyncById (int lojaId){
            IQueryable<Loja> query = _context.Lojas
            .Include(l => l.Endereco);

            query = query.AsNoTracking().OrderByDescending(l => l.Descricao)
            .Where(l => l.Id == lojaId)
            .Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Loja[]> GetAllLojaAsyncByDescricao (string descricao){
            IQueryable<Loja> query = _context.Lojas
            .Include(l => l.Endereco);

            query = query.AsNoTracking().OrderByDescending(c => c.Id)
            .Where(l => l.Descricao.ToLower().Contains(descricao.ToLower()))
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }
    }
}