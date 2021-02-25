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
    public class LojaController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly ILojaRepository _lojaRepository;
        public LojaController(ILojaRepository lojaRepository)
        {
            _lojaRepository = lojaRepository;
        }
        [HttpGet]// api/Loja
        public async Task<IActionResult> Get()
        {
            try
            {
                var loja = await _lojaRepository.GetAllLojaAsync();
                
                return Ok(loja); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("{LojaId}")]// api/Loja/{LojaId}
        public async Task<IActionResult> Get(int LojaId)
        {
            try
            {
                var loja = await _lojaRepository.GetLojaAsyncById(LojaId);              
                return Ok(loja); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByDescricao/{descricao}")]// api/Loja/getByDescricao/{descricao}
        public async Task<IActionResult> Get(string descricao)
        {
            try
            {
                var loja = await _lojaRepository.GetAllLojaAsyncByDescricao(descricao);              
                return Ok(loja); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Loja model)
        {
            try
            {
                model.DataInclusao = DateTime.Now;
                
                _lojaRepository.Add(model);
                
                if(await _lojaRepository.SaveChangesAsync())
                {
                    return Created($"/api/loja/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }
        [HttpPut("{LojaId}")]
        public async Task<IActionResult> Put(int LojaId, Loja model)
        {
            try
            {
                var loja = await _lojaRepository.GetLojaAsyncById(LojaId);
                if(loja == null) return NotFound();

                model.DataInclusao = loja.DataInclusao;

                if(model.Ativo == false)
                    model.DataExclusao = DateTime.Now;

                _lojaRepository.Update(model);
                
                if(await _lojaRepository.SaveChangesAsync())
                {
                    return Created($"/api/loja/{model.Id}", model);
                }                
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }   

            return BadRequest();         
        }

        [HttpDelete("{LojaId}")]
        public async Task<IActionResult> Delete(int LojaId)
        {
            try
            {
                var loja = await _lojaRepository.GetLojaAsyncById(LojaId);
                if(loja == null) return NotFound();

                _lojaRepository.Delete(loja);
                
                if(await _lojaRepository.SaveChangesAsync())
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