using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyBank.Api.Domain.Models;

namespace EasyBank.Api.Domain.Repository.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario, long>
    {
        Task<Usuario?> ObterUsuarioPorEmail(string email);
    }
}