using System;
using System.Threading.Tasks;
using AutoMapper;
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
    public class EstoqueController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IMapper _mapper;
        public EstoqueController(IEstoqueRepository EstoqueRepository, IMapper mapper)
        {
            _mapper = mapper;
            _estoqueRepository = EstoqueRepository;
        }
        [HttpGet()]// api/Estoque/{id}
        public async Task<IActionResult> Get()
        {
            try
            {
                var estoque = await _estoqueRepository.GetAllEstoqueAsync();
                var results = _mapper.Map<EstoqueDto[]>(estoque);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("{EstoqueId}")]// api/Estoque/{id}
        public async Task<IActionResult> Get(int EstoqueId)
        {
            try
            {
                var estoque = await _estoqueRepository.GetEstoqueAsyncById(EstoqueId);
                var results = _mapper.Map<EstoqueDto>(estoque);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByProdutoId/{produtoId}")]// api/Estoque/getByNome/{nome}
        public async Task<IActionResult> ByProdutoId(int produtoId)
        {
            try
            {
                var estoque = await _estoqueRepository.GetEstoqueAsyncByProdutoId(produtoId);

                var results = _mapper.Map<EstoqueDto>(estoque);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EstoqueDto model)
        {
            try
            {
                var estoque = _mapper.Map<Estoque>(model);

                estoque.DataInclusao = DateTime.Now;
                _estoqueRepository.Add(estoque);

                if (await _estoqueRepository.SaveChangesAsync())
                {
                    return Created($"/api/Estoque/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{EstoqueId}")]
        public async Task<IActionResult> Put(int estoqueId, EstoqueDto model)
        {
            try
            {
                var estoque = await _estoqueRepository.GetEstoqueAsyncById(estoqueId);
                if (estoque == null) return NotFound();

                var estoqueNew = _mapper.Map<Estoque>(model);

                estoqueNew.Id = estoqueId;
                estoqueNew.DataInclusao = estoque.DataInclusao;
                estoqueNew.DataExclusao = estoque.DataExclusao;
                estoqueNew.Ativo = estoque.Ativo;

                _estoqueRepository.Update(estoqueNew);

                if (await _estoqueRepository.SaveChangesAsync())
                {
                    return Created($"/api/Estoque/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{EstoqueId}")]
        public async Task<IActionResult> Delete(int estoqueId)
        {
            try
            {
                var estoque = await _estoqueRepository.GetEstoqueAsyncById(estoqueId);
                if (estoque == null) return NotFound();

                estoque.Ativo = false;
                estoque.DataExclusao = DateTime.Now;

                _estoqueRepository.Update(estoque);

                if (await _estoqueRepository.SaveChangesAsync())
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