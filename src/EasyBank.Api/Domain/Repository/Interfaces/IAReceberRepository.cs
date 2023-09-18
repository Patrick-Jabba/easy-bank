using EasyBank.Api.Domain.Models;

namespace EasyBank.Api.Domain.Repository.Interfaces
{
    public interface IAReceberRepository : IRepository<AReceber, long>
    {
        Task<IEnumerable<AReceber>> ObterPeloIdUsuario(long idUsuario);
    }
}