using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProStock.API.Dtos;
using ProStock.API.Helpers;
using ProStock.Domain;
using ProStock.Repository;
using ProStock.Repository.Interfaces;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IEstoqueRepository _estoqueRepository;
        public VendaController(IVendaRepository vendaRepository, IProdutoRepository produtoRepository, IEstoqueRepository estoqueRepository, IMapper mapper)
        {
            _estoqueRepository = estoqueRepository;
            _mapper = mapper;
            _vendaRepository = vendaRepository;
            _produtoRepository = produtoRepository;
        }

        [HttpGet]// api/venda
        public async Task<IActionResult> Get()
        {
            try
            {
                var vendas = await _vendaRepository.GetAllVendasAsync();
                var results = _mapper.Map<VendaDto[]>(vendas);

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }
        }
        [HttpGet("report/general")]
        public async Task<IActionResult> RelatorioGeral([FromQuery] string dateInit, [FromQuery] string dateEnd)
        {
            try
            {
                var vendas = await _vendaRepository.GetVendasAsyncByDate(Util.StringToDate(dateInit), Util.StringToDate(dateEnd));
                if (vendas == null)
                    return NotFound();
                var relatorio = new RelatorioBase();

                foreach(Venda venda in vendas)
                {
                    var aux = new RelatorioVendaDto();
                    aux.Id = venda.Id;
                    aux.Acrescimo = venda.Acrescimo;
                    aux.Cliente = venda.Cliente.Pessoa.Nome;
                    aux.Data = venda.Data;
                    aux.Desconto = venda.Desconto;
                    aux.Frete = venda.Frete;
                    aux.QtdProdutos = venda.ProdutosVendas.Count;
                    aux.Status = venda.Status;
                    aux.Usuario = venda.Usuario.Pessoa.Nome;
                    aux.ValorTotal = venda.ValorTotal;

                    relatorio.Vendas.Add(aux);

                    relatorio.ValorTotalTotal += venda.ValorTotal;
                    relatorio.AcrescimoTotal += venda.Acrescimo;
                    relatorio.DescontoTotal += venda.Desconto;
                    relatorio.FreteTotal += venda.Frete;
                }
                return Ok(relatorio);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }
        }
        [HttpGet("report/product")]
        public async Task<IActionResult> RelatorioProduto([FromQuery] string dateInit, [FromQuery] string dateEnd)
        {
            try
            {
                var vendas = await _vendaRepository.GetProdutosVendidos(Util.StringToDate(dateInit), Util.StringToDate(dateEnd));
                if (vendas == null || vendas.Count == 0)
                    return NotFound();
                var relatorio = new List<RelatorioProdutoDto>();

                foreach(var data in vendas)
                {
                    var aux = new RelatorioProdutoDto();
                    var produto = await _produtoRepository.GetProdutosAsyncById(data.ProdutoId, false);

                    aux.Id = data.ProdutoId;
                    aux.Descricao = produto.Descricao;
                    aux.Nome = produto.Nome;
                    aux.ValorUnit = produto.ValorUnit;
                    aux.Marca = produto.Marca;
                    aux.Quantidade = data.Quantidade;
                    relatorio.Add(aux);
                }

                return Ok(relatorio);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }
        }
        [HttpGet("report/{vendaId}")]
        public async Task<IActionResult> RelatorioGeral(int vendaId)
        {
            try
            {
                var vendas = await _vendaRepository.GetVendasAsyncById(vendaId);
                if (vendas == null)
                    return NotFound();
                var results = _mapper.Map<RelatorioVendaDetalhadaDto>(vendas);
                results.Cliente = vendas.Cliente.Pessoa.Nome;
                results.Usuario = vendas.Usuario.Pessoa.Nome;

                var produtos = _mapper.Map<ProdutoVendaDto[]>(vendas.ProdutosVendas);
                foreach (ProdutoVendaDto data in produtos)
                {
                    var produto = await _produtoRepository.GetProdutosAsyncById(data.ProdutoId, false);
                    if (produto == null) return NotFound();

                    var produtodto = _mapper.Map<ProdutoDto>(produto);
                    produtodto.Quantidade = data.Quantidade;

                    results.Produtos.Add(produtodto);
                }

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }
        }
        [HttpGet("{vendaId}")]// api/venda/{id}
        public async Task<IActionResult> Get(int vendaId)
        {
            try
            {
                var vendas = await _vendaRepository.GetVendasAsyncById(vendaId);
                if (vendas == null)
                    return NotFound();
                var results = _mapper.Map<VendaGetDto>(vendas);

                results.Produtos = new List<ProdutoDto>();
                var produtos = _mapper.Map<ProdutoVendaDto[]>(vendas.ProdutosVendas);
                foreach (ProdutoVendaDto data in produtos)
                {
                    var produto = await _produtoRepository.GetProdutosAsyncById(data.ProdutoId, false);
                    if (produto == null) return NotFound();

                    var produtodto = _mapper.Map<ProdutoDto>(produto);
                    produtodto.Quantidade = data.Quantidade;

                    results.Produtos.Add(produtodto);
                }

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
        }
        [HttpGet("getByUsuario/{UsuarioId}")]
        public async Task<IActionResult> ByUsuarioId(int UsuarioId)
        {
            try
            {
                var vendas = await _vendaRepository.GetAllVendasAsyncByUserId(UsuarioId);

                var results = _mapper.Map<VendaDto[]>(vendas);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("getByCliente/{ClienteId}")]
        public async Task<IActionResult> ByClienteId(int ClienteId)
        {
            try
            {
                var vendas = await _vendaRepository.GetAllVendasAsyncByClientId(ClienteId);

                var results = _mapper.Map<VendaDto[]>(vendas);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(VendaDto model)
        {
            try
            {
                var venda = _mapper.Map<Venda>(model);

                venda.DataInclusao = DateTime.Now;

                venda.ProdutosVendas = new List<ProdutoVenda>();
                foreach (ProdutoVendaDto data in model.Produtos)
                {
                    var produto = await _produtoRepository.GetProdutosAsyncById(data.ProdutoId);
                    if (produto == null) return NotFound();

                    var estoque = await _estoqueRepository.GetEstoqueAsyncByProdutoId(data.ProdutoId);
                    if (estoque == null) return NotFound();

                    if(estoque.QtdMinima > (estoque.QtdAtual - data.Quantidade))
                        return Unauthorized();

                    ProdutoVenda pv = new ProdutoVenda();
                    pv.Produto = produto;
                    pv.Venda = new Venda();
                    pv.ProdutoId = produto.Id;
                    pv.Quantidade = data.Quantidade;
                    venda.ProdutosVendas.Add(pv);
                }

                _vendaRepository.Update(venda);

                if (await _vendaRepository.SaveChangesAsync())
                {
                    return Created($"/api/venda/{venda.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{vendaId}")]
        public async Task<IActionResult> Put(int vendaId, VendaDto model)
        {
            try
            {
                var venda = await _vendaRepository.GetVendasAsyncById(vendaId);
                if (venda == null) return NotFound();

                var vendaNew = _mapper.Map<Venda>(model);

                vendaNew.Id = vendaId;
                vendaNew.DataInclusao = venda.DataInclusao;
                vendaNew.DataExclusao = venda.DataExclusao;
                vendaNew.Ativo = venda.Ativo;

                _vendaRepository.Update(vendaNew);

                if (await _vendaRepository.SaveChangesAsync())
                {
                    return Created($"/api/venda/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{vendaId}")]
        public async Task<IActionResult> Delete(int vendaId)
        {
            try
            {
                var venda = await _vendaRepository.GetVendasAsyncById(vendaId);
                if (venda == null) return NotFound();

                venda.Ativo = false;
                venda.DataExclusao = DateTime.Now;

                _vendaRepository.Update(venda);

                if (await _vendaRepository.SaveChangesAsync())
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