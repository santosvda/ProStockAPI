using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProStockContext _context;
        public ProdutoRepository(ProStockContext context)
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

        // Produtos --------------------------
        public async Task<Produto[]> GetAllProdutosAsync(){
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking().Where(p => p.Ativo).OrderBy(p => p.Id)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Produto[]> GetAllProdutosAsyncByName (string nome){
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking().OrderByDescending(p => p.DataExclusao)
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetProdutosAsyncById (int produtoId, bool activatedObjects = true){
            IQueryable<Produto> query = _context.Produtos;

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.Id == produtoId);
            
            if(activatedObjects)
                query = query.AsNoTracking().Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }
    }
}