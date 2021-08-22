using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ProStockContext _context;
        public PessoaRepository(ProStockContext context)
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
        public async Task<Pessoa[]> GetAllPessoaAsync(){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderBy(p => p.Nome)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Pessoa[]> GetAllPessoaAsyncByName(string nome){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Pessoa> GetPessoaAsyncById (int pessoaId){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Id == pessoaId)
            .Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Pessoa> GetPessoaAsyncByCpf (string cpf){
            IQueryable<Pessoa> query = _context.Pessoas
            .Include(p => p.Enderecos);

            query = query.AsNoTracking().OrderByDescending(p => p.Nome)
            .Where(p => p.Cpf == cpf)
            .Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }
    }
}