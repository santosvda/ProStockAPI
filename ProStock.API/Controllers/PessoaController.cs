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
    public class PessoaController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaController(IPessoaRepository repository)
        {
            _pessoaRepository = repository;
        }

        [HttpGet]// api/pessoa
        public async Task<IActionResult> Get()
        {
            try
            {
                var pessoa = await _pessoaRepository.GetAllPessoaAsync();
                
                return Ok(pessoa); 
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
                var pessoa = await _pessoaRepository.GetPessoaAsyncById(pessoaId);              
                return Ok(pessoa); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByNome/{nome}")]// api/pessoa/getByNome/{nome}
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetAllPessoaAsyncByName(nome);

                return Ok(pessoa); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   
        }
        
        /*[HttpGet("getByCpf/{cpf}")]// api/pessoa/getByCpf/{cpf}
        public async Task<IActionResult> Get(string cpf)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetAllPessoaAsyncByName(cpf);

                return Ok(pessoa); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   
        }*/

        [HttpPost]
        public async Task<IActionResult> Post(Pessoa model)
        {
            try
            {
                model.DataInclusao = DateTime.Now;
                _pessoaRepository.Add(model);
                
                if(await _pessoaRepository.SaveChangesAsync())
                {
                    return Created($"/api/pessoa/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{PessoaId}")]
        public async Task<IActionResult> Put(int PessoaId, Pessoa model)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetPessoaAsyncById(PessoaId);
                if(pessoa == null) return NotFound();

                model.DataInclusao = pessoa.DataInclusao;

                if(model.Ativo == false)
                    model.DataExclusao = DateTime.Now;

                _pessoaRepository.Update(model);
                
                if(await _pessoaRepository.SaveChangesAsync())
                {
                    return Created($"/api/pessoa/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }

        [HttpDelete("{PessoaId}")]
        public async Task<IActionResult> Delete(int PessoaId)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetPessoaAsyncById(PessoaId);
                if(pessoa == null) return NotFound();

                _pessoaRepository.Delete(pessoa);
                
                if(await _pessoaRepository.SaveChangesAsync())
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