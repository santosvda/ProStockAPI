using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain;
using ProStock.Repository;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProStockRepository _repository;
        public TipoUsuarioController(IProStockRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]// api/tipousuario
        public async Task<IActionResult> Get()
        {
            try
            {
                var tipoUsuarios = await _repository.GetAllTipoUsuarioAsync();
                
                return Ok(tipoUsuarios); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("{TipoId}")]// api/tipousuario/{id}
        public async Task<IActionResult> Get(int TipoId)
        {
            try
            {
                var tipoUsuarios = await _repository.GetTipoUsuarioAsyncById(TipoId);              
                return Ok(tipoUsuarios); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByDescricao/{descricao}")]// api/tipousuario/getByDescricao/{descricao}
        public async Task<IActionResult> Get(string descricao)
        {
            try
            {
                var tipoUsuarios = await _repository.GetAllTipoUsuarioAsyncByDescricao(descricao);

                return Ok(tipoUsuarios); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Post(TipoUsuario model)
        {
            try
            {
                _repository.Add(model);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/tipousuario/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{TipoId}")]
        public async Task<IActionResult> Put(int TipoId, TipoUsuario model)
        {
            try
            {
                var tipo = await _repository.GetTipoUsuarioAsyncById(TipoId);
                if(tipo == null) return NotFound();

                _repository.Update(model);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/tipousuario/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }

        [HttpDelete("{TipoId}")]
        public async Task<IActionResult> Delete(int TipoId)
        {
            try
            {
                var tipo = await _repository.GetTipoUsuarioAsyncById(TipoId);
                if(tipo == null) return NotFound();

                _repository.Delete(tipo);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Ok();
                }                
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   

            return BadRequest();         
        }
    }
}