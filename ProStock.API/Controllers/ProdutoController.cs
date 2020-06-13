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
    public class ProdutoController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProStockRepository _repository;
        public ProdutoController(IProStockRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]// api/produto
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await _repository.GetAllProdutosAsync(true);
                
                return Ok(produtos); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("{ProdutoId}")]// api/produto/{id}
        public async Task<IActionResult> Get(int produtoId, bool includeVendas = true)
        {
            try
            {
                var produtos = await _repository.GetProdutosAsyncById(produtoId, includeVendas);              
                return Ok(produtos); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("getByNome/{nome}")]// api/produto/getByNome/{nome}
        public async Task<IActionResult> Get(string nome, bool includeVendas = true)
        {
            try
            {
                var produtos = await _repository.GetAllProdutosAsyncByName(nome, includeVendas);

                return Ok(produtos); 
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }   
        }

        [HttpPost]
        public async Task<IActionResult> Post(Produto model)
        {
            try
            {
                model.DataInclusao = DateTime.Now;
                
                _repository.Add(model);
                
                if(await _repository.SaveChangesAsync())
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
    }
}