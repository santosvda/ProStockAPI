using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.Domain;
using ProStock.Repository;
using ProStock.Repository.Interfaces;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cliente = await _clienteRepository.GetAllClienteAsync();
                
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
                var cliente = await _clienteRepository.GetClienteAsyncById(ClienteId);              
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
                var cliente = await _clienteRepository.GetClienteAsyncByCpf(cpf);              
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
                _clienteRepository.Add(model);
                
                if(await _clienteRepository.SaveChangesAsync())
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
                var cliente = await _clienteRepository.GetClienteAsyncById(ClienteId);
                if(cliente == null) return NotFound();

                //model.Pessoa.DataInclusao = cliente.Pessoa.DataInclusao;

                if(model.Ativo == false)
                    model.Pessoa.DataExclusao = DateTime.Now;

                _clienteRepository.Update(model);
                
                if(await _clienteRepository.SaveChangesAsync())
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
                var cliente = await _clienteRepository.GetClienteAsyncById(ClienteId);
                if(cliente == null) return NotFound();

                _clienteRepository.Delete(cliente);
                
                if(await _clienteRepository.SaveChangesAsync())
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