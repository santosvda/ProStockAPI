using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;

namespace ProStock.Repository
{
    public class ProStockRepository : IProStockRepository
    {
        private readonly ProStockContext _context;
        public ProStockRepository(ProStockContext context)
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
        public async Task<Produto[]> GetAllProdutosAsync(bool includeVendas){
            IQueryable<Produto> query = _context.Produtos
            .Include(p => p.Usuario);

            if(includeVendas){
                query = query.Include(pv => pv.ProdutosVendas)
                .ThenInclude (p => p.Venda);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Produto[]> GetAllProdutosAsyncByName (string nome, bool includeVendas = false){
            IQueryable<Produto> query = _context.Produtos
            .Include(p => p.Usuario);

            if(includeVendas){
                query = query.Include(pv => pv.ProdutosVendas)
                .ThenInclude (p => p.Venda);
            }

            query = query.AsNoTracking().OrderByDescending(p => p.DataExclusao)
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Produto> GetAllProdutosAsyncById (int produtoId, bool includeVendas = false){
            IQueryable<Produto> query = _context.Produtos
            .Include(p => p.Usuario);

            if(includeVendas){
                query = query.Include(pv => pv.ProdutosVendas)
                .ThenInclude (p => p.Venda);
            }

            query = query.AsNoTracking().OrderByDescending(p => p.DataInclusao)
            .Where(p => p.Id == produtoId);

            return await query.FirstOrDefaultAsync();
        }

        //Tipo Usuario --------------------------------------
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

        public async Task<TipoUsuario> GetAllTipoUsuarioAsyncById (int tipoId){
            IQueryable<TipoUsuario> query = _context.TiposUsuarios;

            query = query.AsNoTracking().OrderByDescending(tp => tp.Descricao)
            .Where(tp => tp.Id == tipoId);

            return await query.FirstOrDefaultAsync();
        }

        //Pessoa ----------------------
        public async Task<Pessoa[]> GetAllPessoaAsync(){
            IQueryable<Pessoa> query = _context.Pessoas;

            query = query.AsNoTracking().OrderBy(p => p.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Pessoa[]> GetAllPessoaAsyncByName(string nome){
            IQueryable<Pessoa> query = _context.Pessoas;

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Pessoa> GetAllPessoaAsyncById (int pessoaId){
            IQueryable<Pessoa> query = _context.Pessoas;

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Id == pessoaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Pessoa> GetAllPessoaAsyncByCpf (string cpf){
            IQueryable<Pessoa> query = _context.Pessoas;

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Cpf == cpf);

            return await query.FirstOrDefaultAsync();
        }
        //Endereco ----------------------
        public async Task<Endereco[]> GetAllEnderecoAsync(){
            IQueryable<Endereco> query = _context.Enderecos
            .Include(e => e.Pessoa);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByCep(string cep){
            IQueryable<Endereco> query = _context.Enderecos
            .Include(e => e.Pessoa);

            query = query.AsNoTracking().OrderByDescending(e => e.Id)
            .Where(e => e.Cep.ToLower().Contains(cep.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByCidade (string cidade){
            IQueryable<Endereco> query = _context.Enderecos
            .Include(e => e.Pessoa);

            query = query.AsNoTracking().OrderByDescending(e => e.Cidade)
            .Where(e => e.Cidade.ToLower().Contains(cidade.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByRua (string rua){
            IQueryable<Endereco> query = _context.Enderecos
            .Include(e => e.Pessoa);

            query = query.AsNoTracking().OrderByDescending(e => e.Rua)
            .Where(e => e.Rua.ToLower().Contains(rua.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}