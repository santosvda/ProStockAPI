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

        public async Task<Produto> GetProdutosAsyncById (int produtoId, bool includeVendas = false){
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

        public async Task<TipoUsuario> GetTipoUsuarioAsyncById (int tipoId){
            IQueryable<TipoUsuario> query = _context.TiposUsuarios;

            query = query.AsNoTracking().OrderByDescending(tp => tp.Descricao)
            .Where(tp => tp.Id == tipoId);

            return await query.FirstOrDefaultAsync();
        }

        //Pessoa ----------------------
        public async Task<Pessoa[]> GetAllPessoaAsync(){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderBy(p => p.Nome);

            return await query.ToArrayAsync();
        }

        public async Task<Pessoa[]> GetAllPessoaAsyncByName(string nome){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Pessoa> GetPessoaAsyncById (int pessoaId){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Id == pessoaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Pessoa> GetPessoaAsyncByCpf (string cpf){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Cpf == cpf);

            return await query.FirstOrDefaultAsync();
        }

        //Endereco ----------------------
        public async Task<Endereco[]> GetAllEnderecoAsync(){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByCep(string cep){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.Id)
            .Where(e => e.Cep.ToLower().Contains(cep.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByCidade (string cidade){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.Cidade)
            .Where(e => e.Cidade.ToLower().Contains(cidade.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByRua (string rua){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.Rua)
            .Where(e => e.Rua.ToLower().Contains(rua.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Endereco> GetEnderecoAsyncById (int enderecoId){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.DataInclusao)
            .Where(e => e.Id == enderecoId);

            return await query.FirstOrDefaultAsync();
        }

        //Cliente-------------------
        public async Task<Cliente[]> GetAllClienteAsync(){
            IQueryable<Cliente> query = _context.Clientes
            .Include(c => c.Pessoa).ThenInclude(ce => ce.Enderecos);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Cliente> GetClienteAsyncById (int clienteId){
            IQueryable<Cliente> query = _context.Clientes
            .Include(c => c.Pessoa).ThenInclude(ce => ce.Enderecos);

            query = query.AsNoTracking().OrderByDescending(c => c.Id)
            .Where(c => c.Id == clienteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente> GetClienteAsyncByCpf (string cpf){
            IQueryable<Cliente> query = _context.Clientes
            .Include(c => c.Pessoa).ThenInclude(ce => ce.Enderecos);

            query = query.AsNoTracking().OrderByDescending(c => c.Id)
            .Where(c => c.Pessoa.Cpf.ToLower().Contains(cpf.ToLower()));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente[]> GetAllClienteAsyncByName (string nome){
            IQueryable<Cliente> query = _context.Clientes
            .Include(c => c.Pessoa).ThenInclude(ce => ce.Enderecos);

            query = query.AsNoTracking().OrderByDescending(c => c.Id)
            .Where(c => c.Pessoa.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        //Loja------------------------
        public async Task<Loja[]> GetAllLojaAsync(){
            IQueryable<Loja> query = _context.Lojas
            .Include(l => l.Endereco);

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Loja> GetLojaAsyncById (int lojaId){
            IQueryable<Loja> query = _context.Lojas
            .Include(l => l.Endereco);

            query = query.AsNoTracking().OrderByDescending(l => l.Descricao)
            .Where(l => l.Id == lojaId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Loja[]> GetAllLojaAsyncByDescricao (string descricao){
            IQueryable<Loja> query = _context.Lojas
            .Include(l => l.Endereco);

            query = query.AsNoTracking().OrderByDescending(c => c.Id)
            .Where(l => l.Descricao.ToLower().Contains(descricao.ToLower()));

            return await query.ToArrayAsync();
        }

        //Usuario -------------------------
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