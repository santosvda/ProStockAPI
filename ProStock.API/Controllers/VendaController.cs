using System;
using System.Collections.Generic;
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
    public class VendaController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        public VendaController(IVendaRepository vendaRepository,IProdutoRepository produtoRepository, IMapper mapper)
        {
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
        [HttpGet("{vendaId}")]// api/venda/{id}
        public async Task<IActionResult> Get(int vendaId)
        {
            try
            {
                var vendas = await _vendaRepository.GetVendasAsyncById(vendaId);
                var results = _mapper.Map<VendaDto>(vendas);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
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
                    var produto = await _produtoRepository.GetProdutosAsyncById(data.ProdutoId, false);
                    if (produto == null) return NotFound();

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