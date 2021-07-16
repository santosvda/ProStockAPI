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
    public class PessoaController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;
        public PessoaController(IPessoaRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _pessoaRepository = repository;
        }

        [HttpGet]// api/pessoa
        public async Task<IActionResult> Get()
        {
            try
            {
                var pessoa = await _pessoaRepository.GetAllPessoaAsync();

                var results = _mapper.Map<PessoaDto[]>(pessoa);

                return Ok(results);
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
                var results = _mapper.Map<PessoaDto>(pessoa);

                return Ok(results);
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
                var results = _mapper.Map<PessoaDto[]>(pessoa);

                return Ok(results);
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
        public async Task<IActionResult> Post(PessoaDto model)
        {
            try
            {
                var pessoa = _mapper.Map<Pessoa>(model);

                pessoa.DataInclusao = DateTime.Now;
                _pessoaRepository.Add(pessoa);

                if (await _pessoaRepository.SaveChangesAsync())
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
        public async Task<IActionResult> Put(int PessoaId, PessoaDto model)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetPessoaAsyncById(PessoaId);
                if (pessoa == null) return NotFound();

                var pessoaNew = _mapper.Map<Pessoa>(model);

                pessoaNew.Id = PessoaId;
                pessoaNew.DataInclusao = pessoa.DataInclusao;
                pessoaNew.DataExclusao = pessoa.DataExclusao;
                pessoaNew.Ativo = pessoa.Ativo;

                _pessoaRepository.Update(pessoaNew);

                if (await _pessoaRepository.SaveChangesAsync())
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
                if (pessoa == null) return NotFound();

                pessoa.Ativo = false;
                pessoa.DataExclusao = DateTime.Now;

                _pessoaRepository.Update(pessoa);

                if (await _pessoaRepository.SaveChangesAsync())
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