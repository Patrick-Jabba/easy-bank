using EasyBank.Api.DTO.AReceber;

namespace EasyBank.Api.Domain.Services.Interfaces
{
    public interface IAReceberService : IService<AReceberRequestDTO, AReceberResponseDTO, long>
    {
        Task<IEnumerable<AReceberResponseDTO>> ObterPeloIdUsuario(long idUsuario);

        Task<IEnumerable<AReceberResponseDTO>> Obter(long idUsuario);
    }
}