using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.DTO.Usuario;

namespace EasyBank.Api.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioRequestDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioResponseDTO>().ReverseMap();
        }
    }
}