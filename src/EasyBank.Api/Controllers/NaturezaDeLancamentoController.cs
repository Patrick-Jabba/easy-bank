using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.NaturezaDeLancamento;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBank.Api.Controllers
{
    [ApiController]
    [Route("api/naturezaDeLancamentos/")]
    public class NaturezaDeLancamentoController : BaseController
    {
        private readonly INaturezaDeLancamentoService _naturezaDeLancamentoService;

        private long _idUsuario;

        public NaturezaDeLancamentoController(INaturezaDeLancamentoService naturezaDeLancamentoService)
        {
            _naturezaDeLancamentoService = naturezaDeLancamentoService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(NaturezaDeLancamentoRequestDTO naturezaDeLancamentoRequestDTO)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _naturezaDeLancamentoService.Adicionar(naturezaDeLancamentoRequestDTO, _idUsuario));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, NaturezaDeLancamentoRequestDTO naturezaDeLancamentoDTO)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Atualizar(id, naturezaDeLancamentoDTO, _idUsuario));
            }
           catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Obter()
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.Obter(_idUsuario));
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
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _naturezaDeLancamentoService.ObterPorId(id, _idUsuario));
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
                _idUsuario = ObterIdUsuarioLogado();
               await _naturezaDeLancamentoService.Inativar(id, _idUsuario);

               return NoContent();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}