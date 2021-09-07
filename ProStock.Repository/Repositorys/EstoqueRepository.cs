using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class EstoqueRepository : IEstoqueRepository
    {
        private readonly ProStockContext _context;
        public EstoqueRepository(ProStockContext context)
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

        public async Task<Estoque[]> GetAllEstoqueAsync (){
            IQueryable<Estoque> query = _context.Estoques;

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }


        public async Task<Estoque> GetEstoqueAsyncById (int estoqueId){
            IQueryable<Estoque> query = _context.Estoques;

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.Id == estoqueId)
            .Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Estoque> GetEstoqueAsyncByProdutoId (int produtoId){
            IQueryable<Estoque> query = _context.Estoques;

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.ProdutoId == produtoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}