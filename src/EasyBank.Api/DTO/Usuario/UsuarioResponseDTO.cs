using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBank.Api.DTO.Usuario
{
    public class UsuarioResponseDTO : UsuarioRequestDTO
    {
        public long Id {get; set;}
        public DateTime DataCadastro {get; set;}
    }
}