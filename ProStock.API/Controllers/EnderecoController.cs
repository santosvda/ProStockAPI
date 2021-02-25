using System;
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
    public class EnderecoController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoController(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }
        [HttpGet]// api/endereco
        public async Task<IActionResult> Get()
        {
            try
            {
                var endereco = await _enderecoRepository.GetAllEnderecoAsync();
                
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
                var endereco = await _enderecoRepository.GetEnderecoAsyncById(EnderecoId);              
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
                var endereco = await _enderecoRepository.GetAllEnderecoAsyncByCep(cep);              
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
                
                _enderecoRepository.Add(model);
                
                if(await _enderecoRepository.SaveChangesAsync())
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
                var endereco = await _enderecoRepository.GetEnderecoAsyncById(EnderecoId);
                if(endereco == null) return NotFound();

                model.DataInclusao = endereco.DataInclusao;

                if(model.Ativo == false)
                    model.DataExclusao = DateTime.Now;

                _enderecoRepository.Update(model);
                
                if(await _enderecoRepository.SaveChangesAsync())
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
                var endereco = await _enderecoRepository.GetEnderecoAsyncById(EnderecoId);
                if(endereco == null) return NotFound();

                _enderecoRepository.Delete(endereco);
                
                if(await _enderecoRepository.SaveChangesAsync())
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