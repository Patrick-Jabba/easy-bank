using System.Security.Authentication;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBank.Api.Controllers
{
    [ApiController]
    [Route("api/usuarios/")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar(UsuarioRequestDTO usuarioRequestDto)
        {
            try
            {
                return Ok(await _usuarioService.Autenticar(usuarioRequestDto));
            }
            catch( AuthenticationException ex)
            {
                return Unauthorized(new { statusCode = 401, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(UsuarioRequestDTO usuarioRequestDto)
        {
            try
            {
                return Created("", await _usuarioService.Adicionar(usuarioRequestDto, 0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Obter()
        {
            try
            {
                return Ok(await _usuarioService.Obter());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("porEmail/{email}")]
        [Authorize]
        public async Task<IActionResult> ObterUsuarioPorEmail(string email)
        {
            try
            {
                return Ok(await _usuarioService.ObterUsuarioPorEmail(email));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Route("porId/{id}")]
        [Authorize]
        public async Task<IActionResult> ObterPorId(long id)
        {
            try
            {
                return Ok(await _usuarioService.ObterPorId(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, UsuarioRequestDTO usuarioDto)
        {
            try
            {
                return Ok(await _usuarioService.Atualizar(id, usuarioDto, 0));
            }
           catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Deletar(long id)
        {
            try
            {
               await _usuarioService.Inativar(id);

               return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}