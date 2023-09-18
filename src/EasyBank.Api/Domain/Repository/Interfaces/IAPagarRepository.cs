using EasyBank.Api.Domain.Models;

namespace EasyBank.Api.Domain.Repository.Interfaces
{
    public interface IAPagarRepository : IRepository<APagar, long>
    {
        Task<IEnumerable<APagar>> ObterPeloIdUsuario(long idUsuario);
    }
}