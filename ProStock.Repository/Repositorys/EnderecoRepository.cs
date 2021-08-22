using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProStock.Domain;
using ProStock.Repository.Interfaces;

namespace ProStock.Repository.Repositorys
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly ProStockContext _context;
        public EnderecoRepository(ProStockContext context)
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

        public async Task<Endereco[]> GetAllEnderecoAsync(){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderBy(e => e.Id)
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByCep(string cep){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.Id)
            .Where(e => e.Cep.ToLower().Contains(cep.ToLower()))
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByCidade (string cidade){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.Cidade)
            .Where(e => e.Cidade.ToLower().Contains(cidade.ToLower()))
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Endereco[]> GetAllEnderecoAsyncByRua (string rua){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.Rua)
            .Where(e => e.Rua.ToLower().Contains(rua.ToLower()))
            .Where(e => e.Ativo);

            return await query.ToArrayAsync();
        }

        public async Task<Endereco> GetEnderecoAsyncById (int enderecoId){
            IQueryable<Endereco> query = _context.Enderecos;

            query = query.AsNoTracking().OrderByDescending(e => e.DataInclusao)
            .Where(e => e.Id == enderecoId)
            .Where(e => e.Ativo);

            return await query.FirstOrDefaultAsync();
        }

    }
}