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
    public class ProdutoController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]// api/produto
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutosAsync();
                var results = _mapper.Map<ProdutoDto[]>(produtos);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }
        }
        [HttpGet("{ProdutoId}")]// api/produto/{id}
        public async Task<IActionResult> Get(int produtoId)
        {
            try
            {
                var produtos = await _produtoRepository.GetProdutosAsyncById(produtoId);
                var results = _mapper.Map<ProdutoDto>(produtos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByNome/{nome}")]// api/produto/getByNome/{nome}
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var produtos = await _produtoRepository.GetAllProdutosAsyncByName(nome);

                var results = _mapper.Map<ProdutoDto[]>(produtos);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProdutoDto model)
        {
            try
            {
                var produto = _mapper.Map<Produto>(model);

                produto.DataInclusao = DateTime.Now;
                _produtoRepository.Add(produto);

                if (await _produtoRepository.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{ProdutoId}")]
        public async Task<IActionResult> Put(int ProdutoId, ProdutoDto model)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                var produtoNew = _mapper.Map<Produto>(model);

                produtoNew.Id = ProdutoId;
                produtoNew.DataInclusao = produto.DataInclusao;
                produtoNew.DataExclusao = produto.DataExclusao;
                produtoNew.Ativo = produto.Ativo;

                _produtoRepository.Update(produtoNew);

                if (await _produtoRepository.SaveChangesAsync())
                {
                    return Created($"/api/produto/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{ProdutoId}")]
        public async Task<IActionResult> Delete(int ProdutoId)
        {
            try
            {
                var produto = await _produtoRepository.GetProdutosAsyncById(ProdutoId);
                if (produto == null) return NotFound();

                produto.Ativo = false;
                produto.DataExclusao = DateTime.Now;

                _produtoRepository.Update(produto);

                if (await _produtoRepository.SaveChangesAsync())
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