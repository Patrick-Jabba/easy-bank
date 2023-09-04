using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBank.Api.Domain.Repository.Interfaces
{
    public interface IRepository<T, I> where T : class
    {
        Task<IEnumerable<T>> Obter();
        Task<T?> ObterPorId(I id);
        Task<T> Adicionar(T obj);
        Task<T?> Atualizar(T obj);
        Task Deletar(T obj);

    }
}