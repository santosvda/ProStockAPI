using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain;
using ProStock.Repository;
using ProStock.Repository.Interfaces;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly ITipoUsuarioRepository _tipoUsuarioRepository;
        public TipoUsuarioController(ITipoUsuarioRepository repository)
        {
            _tipoUsuarioRepository = repository;
        }

        [HttpGet]// api/tipousuario
        public async Task<IActionResult> Get()
        {
            try
            {
                var tipoUsuarios = await _tipoUsuarioRepository.GetAllTipoUsuarioAsync();
                
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
                var tipoUsuarios = await _tipoUsuarioRepository.GetTipoUsuarioAsyncById(TipoId);              
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
                var tipoUsuarios = await _tipoUsuarioRepository.GetAllTipoUsuarioAsyncByDescricao(descricao);

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
                _tipoUsuarioRepository.Add(model);
                
                if(await _tipoUsuarioRepository.SaveChangesAsync())
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
                var tipo = await _tipoUsuarioRepository.GetTipoUsuarioAsyncById(TipoId);
                if(tipo == null) return NotFound();

                _tipoUsuarioRepository.Update(model);
                
                if(await _tipoUsuarioRepository.SaveChangesAsync())
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
                var tipo = await _tipoUsuarioRepository.GetTipoUsuarioAsyncById(TipoId);
                if(tipo == null) return NotFound();

                _tipoUsuarioRepository.Delete(tipo);
                
                if(await _tipoUsuarioRepository.SaveChangesAsync())
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