using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain;
using ProStock.Repository;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProStockRepository _repository;
        public ClienteController(IProStockRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cliente = await _repository.GetAllClienteAsync();
                
                return Ok(cliente); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("{ClienteId}")]// api/cliente/{ClienteId}
        public async Task<IActionResult> Get(int ClienteId)
        {
            try
            {
                var cliente = await _repository.GetClienteAsyncById(ClienteId);              
                return Ok(cliente); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("getByCpf/{cpf}")]// api/cliente/{cpf}
        public async Task<IActionResult> Get(string cpf)
        {
            try
            {
                var cliente = await _repository.GetClienteAsyncByCpf(cpf);              
                return Ok(cliente); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cliente model)
        {
            try
            {   
                model.Pessoa.DataInclusao = DateTime.Now;
                _repository.Add(model);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, Cliente model)
        {
            try
            {
                var cliente = await _repository.GetClienteAsyncById(ClienteId);
                if(cliente == null) return NotFound();

                //model.Pessoa.DataInclusao = cliente.Pessoa.DataInclusao;

                if(model.Ativo == false)
                    model.Pessoa.DataExclusao = DateTime.Now;

                _repository.Update(model);
                
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }

        [HttpDelete("{ClienteId}")]
        public async Task<IActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _repository.GetClienteAsyncById(ClienteId);
                if(cliente == null) return NotFound();

                _repository.Delete(cliente);
                
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