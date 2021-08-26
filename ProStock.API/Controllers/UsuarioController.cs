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
using ProStock.API.Helpers;
using ProStock.Domain;
using ProStock.Repository;
using ProStock.Repository.Interfaces;

namespace ProStock.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase //herda para trabalhar com http e etc
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _usuarioRepository = usuarioRepository;
        }
        [HttpGet]// api/usuario
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllUsuarioAsync();

                var results = _mapper.Map<UsuarioGetDto[]>(usuarios);

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
                var usuario = await _usuarioRepository.GetUsuarioAsyncById(UsuarioId);

                var results = _mapper.Map<UsuarioGetDto>(usuario);

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
                var usuario = await _usuarioRepository.GetAllUsuarioAsyncByLogin(Login);

                var results = _mapper.Map<UsuarioGetDto[]>(usuario);

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(UsuarioDto model)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(model);

                usuario.Senha = Encrypt.EncodePasswordToBase64(usuario.Senha);

                usuario.DataInclusao = DateTime.Now;
                _usuarioRepository.Add(usuario);

                if (await _usuarioRepository.SaveChangesAsync())
                {
                    return Created($"/api/usuario/{model.Id}", _mapper.Map<UsuarioGetDto>(model));
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
                var usuario = await _usuarioRepository.GetUsuarioAsyncById(UsuarioId);
                if (usuario == null) return NotFound();

                var usuarioNew = _mapper.Map<Usuario>(model);

                usuarioNew.Id = UsuarioId;
                usuarioNew.DataInclusao = usuario.DataInclusao;
                usuarioNew.DataExclusao = usuario.DataExclusao;
                usuarioNew.Ativo = usuario.Ativo;
                usuarioNew.Senha = usuario.Senha;

                _usuarioRepository.Update(usuarioNew);

                if (await _usuarioRepository.SaveChangesAsync())
                {
                    return Created($"/api/usuario/{model.Id}", _mapper.Map<UsuarioGetDto>(model));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco Dados Falhou " + ex.Message);
            }

            return BadRequest();
        }
        [HttpPut("changeSenha/{UsuarioId}")]
        public async Task<IActionResult> PutSenha(int UsuarioId, UsuarioSenhaDto model)
        {
            try
            {

                if(model.UsuarioId == 0)
                {

                    var usuario = await _usuarioRepository.GetUsuarioAsyncById(UsuarioId);
                    usuario.Senha = model.ConfirmarSenha;
                    usuario.Senha = Encrypt.EncodePasswordToBase64(usuario.Senha);
                    usuario = await _usuarioRepository.Login(usuario);
                    if (usuario == null) return NotFound();

                    var usuarioNew = usuario;
                    usuarioNew.Senha = model.Senha;
                    
                    usuarioNew.Senha = Encrypt.EncodePasswordToBase64(usuarioNew.Senha);

                    _usuarioRepository.Update(usuarioNew);

                    if (await _usuarioRepository.SaveChangesAsync())
                    {
                        return Created($"/api/usuario/{model.Id}", _mapper.Map<UsuarioGetDto>(model));
                    }
                }
            else
            {

                var admin = await _usuarioRepository.GetUsuarioAsyncById(model.UsuarioId);
                if (admin.TipoUsuario != Domain.Enums.TipoUsuario.Admin)
                    return Unauthorized();

                var usuario = await _usuarioRepository.GetUsuarioAsyncById(UsuarioId);
                if (usuario == null) return NotFound();

                var usuarioNew = usuario;
                usuarioNew.Senha = model.Senha;
                usuarioNew.Senha = Encrypt.EncodePasswordToBase64(usuarioNew.Senha);


                _usuarioRepository.Update(usuarioNew);

                if (await _usuarioRepository.SaveChangesAsync())
                {
                    return Created($"/api/usuario/{model.Id}", _mapper.Map<UsuarioGetDto>(model));
                }
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
                var usuario = await _usuarioRepository.GetUsuarioAsyncById(UsuarioId);
                if (usuario == null) return NotFound();

                usuario.Ativo = false;
                usuario.DataExclusao = DateTime.Now;

                _usuarioRepository.Update(usuario);

                if (await _usuarioRepository.SaveChangesAsync())
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
        public async Task<IActionResult> Login(UsuarioLoginDto userLogin)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(userLogin);
                usuario.Senha = Encrypt.EncodePasswordToBase64(usuario.Senha);


                var usuarioLogin = await _usuarioRepository.Login(usuario);
                if (usuarioLogin == null) return Unauthorized();

                var results = _mapper.Map<UsuarioGetDto>(usuarioLogin);

                return Ok(new
                {
                    token = GenerateJWToken(usuarioLogin).Result,
                    user = results
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