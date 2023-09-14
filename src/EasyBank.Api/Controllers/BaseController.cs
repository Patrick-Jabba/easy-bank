using System.Security.Claims;
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
    }
}