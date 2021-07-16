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
    public class ClienteController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cliente = await _clienteRepository.GetAllClienteAsync();

                 var results = _mapper.Map<ClienteDto[]>(cliente);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }
        [HttpGet("{ClienteId}")]// api/cliente/{ClienteId}
        public async Task<IActionResult> Get(int ClienteId)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteAsyncById(ClienteId);
                
                var results = _mapper.Map<ClienteDto>(cliente);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("getByCpf/{cpf}")]// api/cliente/{cpf}
        public async Task<IActionResult> Get(string cpf)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteAsyncByCpf(cpf);

                var results = _mapper.Map<ClienteDto>(cliente);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto model)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(model);

                cliente.DataInclusao = DateTime.Now;
                _clienteRepository.Add(cliente);

                if (await _clienteRepository.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, ClienteDto model)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteAsyncById(ClienteId);
                if (cliente == null) return NotFound();

                var clienteNew = _mapper.Map<Cliente>(model);

                clienteNew.Id = ClienteId;
                clienteNew.DataInclusao = cliente.DataInclusao;
                clienteNew.DataExclusao = cliente.DataExclusao;
                clienteNew.Ativo = cliente.Ativo;

                _clienteRepository.Update(clienteNew);

                if (await _clienteRepository.SaveChangesAsync())
                {
                    return Created($"/api/cliente/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{ClienteId}")]
        public async Task<IActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _clienteRepository.GetClienteAsyncById(ClienteId);
                if (cliente == null) return NotFound();

                cliente.Ativo = false;
                cliente.DataExclusao = DateTime.Now;

                _clienteRepository.Update(cliente);

                if (await _clienteRepository.SaveChangesAsync())
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