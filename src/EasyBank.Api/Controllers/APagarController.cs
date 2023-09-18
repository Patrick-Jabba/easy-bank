
using System.ComponentModel.DataAnnotations;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.APagar;
using EasyBank.Api.DTO.ModelErrorDTO;
using EasyBank.Api.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBank.Api.Controllers
{
    [ApiController]
    [Route("api/titulosapagar")]
    public class APagarController : BaseController
    {
        private readonly IAPagarService _aPagarService;

        private long _idUsuario;

        public APagarController(IAPagarService aPagarService)
        {
            _aPagarService = aPagarService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(APagarRequestDTO aPagarRequestDTO)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Created("", await _aPagarService.Adicionar(aPagarRequestDTO, _idUsuario));
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
        public async Task<IActionResult> Atualizar(long id, APagarRequestDTO aPagarDTO)
        {
            try
            {
                _idUsuario = ObterIdUsuarioLogado();
                return Ok(await _aPagarService.Atualizar(id, aPagarDTO, _idUsuario));
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
                return Ok(await _aPagarService.Obter(_idUsuario));
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
                return Ok(await _aPagarService.ObterPorId(id, _idUsuario));
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
               await _aPagarService.Inativar(id, _idUsuario);

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