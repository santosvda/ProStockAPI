using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class VendaRepository : IVendaRepository
    {
        private readonly ProStockContext _context;
        public VendaRepository(ProStockContext context)
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
        public async Task<Venda[]> GetAllVendasAsync(){
            IQueryable<Venda> query = _context.Vendas;

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Venda[]> GetAllVendasAsyncByUserId (int userId){
            IQueryable<Venda> query = _context.Vendas;

            query = query.AsNoTracking().OrderByDescending(p => p.DataExclusao)
            .Where(p => p.UsuarioId == userId);

            return await query.ToArrayAsync();
        }

        public async Task<Venda[]> GetAllVendasAsyncByClientId (int clientId){
            IQueryable<Venda> query = _context.Vendas;

            query = query.AsNoTracking().OrderByDescending(p => p.DataExclusao)
            .Where(p => p.ClienteId == clientId);

            return await query.ToArrayAsync();
        }

        public async Task<Venda> GetVendasAsyncById (int VendaId){
            IQueryable<Venda> query = _context.Vendas;

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.Id == VendaId);

            return await query.FirstOrDefaultAsync();
        }

    }
}