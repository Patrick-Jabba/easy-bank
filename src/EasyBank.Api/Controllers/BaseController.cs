using System.Security.Claims;
using EasyBank.Api.DTO.ModelErrorDTO;
using Microsoft.AspNetCore.Mvc;

namespace EasyBank.Api.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected long ObterIdUsuarioLogado()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return long.TryParse(id, out var idUsuario) ? idUsuario : 0;
        }

        protected ModelErrorDTO RetornarModelBadRequest(Exception ex)
        {
            return new ModelErrorDTO 
            {
                StatusCode = 400,
                    Titulo = "Bad Request",
                    Mensagem = ex.Message,
                    MomentoOcorrencia = DateTime.Now
            };
        }

        protected ModelErrorDTO RetornarModelNotFound(Exception ex)
        {
            return new ModelErrorDTO 
            {
                StatusCode = 404,
                    Titulo = "Not found",
                    Mensagem = ex.Message,
                    MomentoOcorrencia = DateTime.Now
            };
        }

        protected ModelErrorDTO RetornarModelUnauthorized(Exception ex)
        {
            return new ModelErrorDTO 
            {
                StatusCode = 401,
                    Titulo = "Unauthorized",
                    Mensagem = ex.Message,
                    MomentoOcorrencia = DateTime.Now
            };
        }
    }
}