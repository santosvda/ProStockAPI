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
    public class EnderecoController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;
        public EnderecoController(IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
        }
        [HttpGet]// api/endereco
        public async Task<IActionResult> Get()
        {
            try
            {
                var endereco = await _enderecoRepository.GetAllEnderecoAsync();

                 var results = _mapper.Map<EnderecoDto[]>(endereco);

                return Ok(results);
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
                var results = _mapper.Map<EnderecoDto>(endereco);

                return Ok(results);
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
                
                var results = _mapper.Map<EnderecoDto[]>(endereco);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(EnderecoDto model)
        {
            try
            {
                var endereco = _mapper.Map<Endereco>(model);

                endereco.DataInclusao = DateTime.Now;
                _enderecoRepository.Add(endereco);

                if (await _enderecoRepository.SaveChangesAsync())
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
        public async Task<IActionResult> Put(int EnderecoId, EnderecoDto model)
        {
            try
            {
                var endereco = await _enderecoRepository.GetEnderecoAsyncById(EnderecoId);
                if (endereco == null) return NotFound();

                var enderecoNew = _mapper.Map<Endereco>(model);

                enderecoNew.Id = EnderecoId;
                enderecoNew.DataInclusao = endereco.DataInclusao;
                enderecoNew.DataExclusao = endereco.DataExclusao;
                enderecoNew.Ativo = endereco.Ativo;

                _enderecoRepository.Update(enderecoNew);

                if (await _enderecoRepository.SaveChangesAsync())
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
                if (endereco == null) return NotFound();

                endereco.Ativo = false;
                endereco.DataExclusao = DateTime.Now;

                _enderecoRepository.Update(endereco);

                if (await _enderecoRepository.SaveChangesAsync())
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