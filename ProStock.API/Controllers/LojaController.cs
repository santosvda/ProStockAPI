using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.API.Dtos;
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
        private readonly IMapper _mapper;
        public LojaController(ILojaRepository lojaRepository, IMapper mapper)
        {
            _mapper = mapper;
            _lojaRepository = lojaRepository;
        }
        [HttpGet]// api/Loja
        public async Task<IActionResult> Get()
        {
            try
            {
                var lojas = await _lojaRepository.GetAllLojaAsync();

                 var results = _mapper.Map<LojaDto[]>(lojas);

                return Ok(results);
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

                var results = _mapper.Map<LojaDto>(loja);
                
                return Ok(results);
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

                var results = _mapper.Map<LojaDto[]>(loja);
                
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(LojaDto model)
        {
            try
            {
                var loja = _mapper.Map<Loja>(model);

                loja.DataInclusao = DateTime.Now;
                _lojaRepository.Add(loja);

                if (await _lojaRepository.SaveChangesAsync())
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
        public async Task<IActionResult> Put(int LojaId, LojaDto model)
        {
            try
            {
                var loja = await _lojaRepository.GetLojaAsyncById(LojaId);
                if (loja == null) return NotFound();

                var lojaNew = _mapper.Map<Loja>(model);

                lojaNew.Id = LojaId;
                lojaNew.DataInclusao = loja.DataInclusao;
                lojaNew.DataExclusao = loja.DataExclusao;
                lojaNew.Ativo = loja.Ativo;

                _lojaRepository.Update(lojaNew);

                if (await _lojaRepository.SaveChangesAsync())
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
                if (loja == null) return NotFound();

                loja.Ativo = false;
                loja.DataExclusao = DateTime.Now;

                _lojaRepository.Update(loja);

                if (await _lojaRepository.SaveChangesAsync())
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