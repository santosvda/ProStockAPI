using System;
using System.Globalization;
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
            IQueryable<Venda> query = _context.Vendas
            .Include(v => v.Cliente)
            .ThenInclude(v => v.Pessoa)
            .Include(v => v.Usuario)
            .ThenInclude(v => v.Pessoa);

            query = query.Include(pv => pv.ProdutosVendas);

            query = query.AsNoTracking().OrderBy(p => p.Id)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }
        public async Task<Venda[]> GetAllVendasAsyncByUserId (int userId){
            IQueryable<Venda> query = _context.Vendas
            .Include(v => v.Cliente)
            .ThenInclude(v => v.Pessoa)
            .Include(v => v.Usuario)
            .ThenInclude(v => v.Pessoa);

            query = query.AsNoTracking().OrderByDescending(p => p.DataExclusao)
            .Where(p => p.UsuarioId == userId)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Venda[]> GetAllVendasAsyncByClientId (int clientId){
            IQueryable<Venda> query = _context.Vendas
            .Include(v => v.Cliente)
            .ThenInclude(v => v.Pessoa)
            .Include(v => v.Usuario)
            .ThenInclude(v => v.Pessoa);


            query = query.AsNoTracking().OrderByDescending(p => p.DataExclusao)
            .Where(p => p.ClienteId == clientId)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Venda> GetVendasAsyncById (int VendaId){
            IQueryable<Venda> query = _context.Vendas
            .Include(v => v.Cliente)
            .ThenInclude(v => v.Pessoa)
            .Include(v => v.Usuario)
            .ThenInclude(v => v.Pessoa);

            query = query.Include(pv => pv.ProdutosVendas).ThenInclude(p => p.Produto);


            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.Id == VendaId)
            .Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Venda[]> GetVendasAsyncByDate(DateTime init, DateTime end)
        {
            IQueryable<Venda> query = _context.Vendas
            .Include(v => v.Cliente)
            .ThenInclude(v => v.Pessoa)
            .Include(v => v.Usuario)
            .ThenInclude(v => v.Pessoa);

            query = query.Include(pv => pv.ProdutosVendas);

            query = query.AsNoTracking().OrderBy(p => p.Id)
            .Where(e => e.Data >= init && e.Data <= end)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }
        public async Task<dynamic> GetProdutosVendidos(DateTime init, DateTime end)
        {
            var result = await _context.ProdutosVendas
                .FromSql($@"SELECT 0 as VendaId, pv.ProdutoId, SUM(pv.Quantidade) AS Quantidade, 0 as Id
                            from produtosvendas pv 
                            inner join produtos p on pv.ProdutoId = p.Id
                            inner join vendas v on pv.VendaId = v.Id
                            WHERE v.`Data` between 
                                STR_TO_DATE({init.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}, '%d/%m/%Y') 
                                AND STR_TO_DATE({end.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}, '%d/%m/%Y')
                            group by p.Id").ToListAsync();
            return result;
        }
        public async Task<Produto[]> Get (int VendaId){
            IQueryable<Produto> query = _context.Produtos;

            query = query.Include(pv => pv.ProdutosVendas);

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.Id == VendaId)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

    }
}