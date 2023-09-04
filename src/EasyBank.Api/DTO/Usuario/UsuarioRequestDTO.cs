using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBank.Api.DTO.Usuario
{
    public class UsuarioRequestDTO : UsuarioLoginRequestDTO
    {
        public DateTime? DataInativacao {get; set;}
    }
}