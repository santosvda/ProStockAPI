using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProStock.API.Dtos;
using ProStock.Domain;
using ProStock.Repository;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IProStockRepository _repository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public UsuarioController(IProStockRepository repository, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repository = repository;
        }
        [HttpGet]// api/usuario
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = await _repository.GetAllUsuarioAsync();

                var results = _mapper.Map<UsuarioDto[]>(usuarios);//_mapper.Map<EventoDto[]>(eventos); works too!

                return Ok(results);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
        }

        [HttpGet("{UsuarioId}")]// api/usuario/{id}
        public async Task<IActionResult> Get(int UsuarioId)
        {
            try
            {
                var usuario = await _repository.GetUsuarioAsyncById(UsuarioId);

                var results = _mapper.Map<UsuarioDto[]>(usuario);//_mapper.Map<EventoDto[]>(eventos); works too!

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpGet("getByLogin/{Login}")]// api/usuario/getByLogin/{Login}
        public async Task<IActionResult> Get(string Login)
        {
            try
            {
                var usuario = await _repository.GetAllUsuarioAsyncByLogin(Login);

                var results = _mapper.Map<UsuarioDto[]>(usuario);//_mapper.Map<EventoDto[]>(eventos); works too!

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UsuarioDto model)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(model);

                model.DataInclusao = DateTime.Now;
                _repository.Add(usuario);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/usuario/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou" + ex.Message);
            }
            return BadRequest();
        }
        [HttpPut("{UsuarioId}")]
        public async Task<IActionResult> Put(int UsuarioId, UsuarioDto model)
        {
            try
            {
                var usuario = await _repository.GetUsuarioAsyncById(UsuarioId);
                if (usuario == null) return NotFound();

                model.DataInclusao = usuario.DataInclusao;

                if (model.Ativo == false)
                    model.DataExclusao = DateTime.Now;

                _repository.Update(model);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/usuario/{model.Id}", model);
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }

        [HttpDelete("{UsuarioId}")]
        public async Task<IActionResult> Delete(int UsuarioId)
        {
            try
            {
                var usuario = await _repository.GetUsuarioAsyncById(UsuarioId);
                if (usuario == null) return NotFound();

                _repository.Delete(usuario);

                if (await _repository.SaveChangesAsync())
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

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario userLogin)
        {
            try
            {
                var usuarioLogin = await _repository.Login(userLogin);
                if (usuarioLogin == null) return Unauthorized();

                return Ok(new
                {
                    token = GenerateJWToken(usuarioLogin).Result,
                    user = usuarioLogin
                });
            }
            catch (System.Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"BD falhou {ex.Message}");

            }
        }

        private async Task<string> GenerateJWToken(Usuario user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Login)
            };


            var key = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}