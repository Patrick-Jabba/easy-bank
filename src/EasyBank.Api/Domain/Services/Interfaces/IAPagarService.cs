using EasyBank.Api.DTO.APagar;

namespace EasyBank.Api.Domain.Services.Interfaces
{
    public interface IAPagarService : IService<APagarRequestDTO, APagarResponseDTO, long>
    {
        Task<IEnumerable<APagarResponseDTO>> ObterPeloIdUsuario(long idUsuario);

        Task<IEnumerable<APagarResponseDTO>> Obter(long idUsuario);
    }
}