using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyBank.Api.DTO.NaturezaDeLancamento;

namespace EasyBank.Api.Domain.Services.Interfaces
{
    public interface INaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestDTO, NaturezaDeLancamentoResponseDTO, long>
    {
        Task<IEnumerable<NaturezaDeLancamentoResponseDTO>> ObterPeloIdUsuario(long idUsuario);

        Task<IEnumerable<NaturezaDeLancamentoResponseDTO>> Obter(long idUsuario);
    }
}