using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain;
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
        [HttpGet("{EnderecoId}")]// api/endereco/{EnderecoId}
        public async Task<IActionResult> Get(int EnderecoId)
        {
            try
            {
                var endereco = await _repository.GetEnderecoAsyncById(EnderecoId);              
                return Ok(endereco); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByCep/{cep}")]// api/endereco/getByBep/{cep}
        public async Task<IActionResult> Get(string cep)
        {
            try
            {
                var endereco = await _repository.GetAllEnderecoAsyncByCep(cep);              
                return Ok(endereco); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Endereco model)
        {
            try
            {
                model.DataInclusao = DateTime.Now;
                
                _repository.Add(model);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/endereco/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{EnderecoId}")]
        public async Task<IActionResult> Put(int EnderecoId, Endereco model)
        {
            try
            {
                var endereco = await _repository.GetEnderecoAsyncById(EnderecoId);
                if(endereco == null) return NotFound();

                model.DataInclusao = endereco.DataInclusao;

                if(model.Ativo == false)
                    model.DataExclusao = DateTime.Now;

                _repository.Update(model);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/endereco/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }

        [HttpDelete("{EnderecoId}")]
        public async Task<IActionResult> Delete(int EnderecoId)
        {
            try
            {
                var endereco = await _repository.GetEnderecoAsyncById(EnderecoId);
                if(endereco == null) return NotFound();

                _repository.Delete(endereco);
                
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