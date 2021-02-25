using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ProStockContext _context;
        public ClienteRepository(ProStockContext context)
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
    }
}