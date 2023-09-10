using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.Usuario;
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Inativar(long id)
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