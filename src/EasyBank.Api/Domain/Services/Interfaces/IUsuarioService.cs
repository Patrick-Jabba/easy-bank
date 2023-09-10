using EasyBank.Api.DTO.Usuario;

namespace EasyBank.Api.Domain.Services.Interfaces
{
    public interface IUsuarioService : IService<UsuarioRequestDTO, UsuarioResponseDTO, long>
    {
        Task<UsuarioLoginResponseDTO> Autenticar(UsuarioLoginRequestDTO usuarioLoginRequest);

    }
}