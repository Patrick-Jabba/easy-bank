using EasyBank.Api.Domain.Services.Classes;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace EasyBank.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                return Ok(await _usuarioService.Obter(0));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}