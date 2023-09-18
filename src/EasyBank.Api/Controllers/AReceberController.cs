
using System.ComponentModel.DataAnnotations;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.AReceber;
using EasyBank.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBank.Api.Controllers
{
    [ApiController]
    [Route("api/titulosareceber")]
    public class AReceberController : BaseController
    {
        private readonly IAReceberService _aReceberService;

        private long _idUsuario;

        public AReceberController(IAReceberService aReceberService)
        {
            _aReceberService = aReceberService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(AReceberRequestDTO aReceberRequestDTO)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _aReceberService.Adicionar(aReceberRequestDTO, _idUsuario));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Atualizar(long id, AReceberRequestDTO aReceberDTO)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _aReceberService.Atualizar(id, aReceberDTO, _idUsuario));
            }
             catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(RetornarModelBadRequest(ex));
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
                return Ok(await _aReceberService.Obter(_idUsuario));
            }
             catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
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
                return Ok(await _aReceberService.ObterPorId(id, _idUsuario));
            }
             catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
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
               await _aReceberService.Inativar(id, _idUsuario);

               return NoContent();
            }
             catch (NotFoundException ex)
            {
                return NotFound(RetornarModelNotFound(ex));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}