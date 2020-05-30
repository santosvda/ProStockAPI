using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Repository;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProStockRepository _repository;
        public EnderecoController(IProStockRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]// api/endereco
        public async Task<IActionResult> Get()
        {
            try
            {
                var endereco = await _repository.GetAllEnderecoAsync();
                
                return Ok(endereco); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("{PessoaId}")]// api/pessoa/{id}
        public async Task<IActionResult> Get(int pessoaId)
        {
            try
            {
                var pessoa = await _repository.GetAllPessoaAsyncById(pessoaId);              
                return Ok(pessoa); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
    }
}