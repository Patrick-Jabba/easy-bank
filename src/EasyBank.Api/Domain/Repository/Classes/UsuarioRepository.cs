using EasyBank.Api.Data;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyBank.Api.Domain.Repository.Classes
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Usuario> Adicionar(Usuario obj)
        {
            await _context.Usuarios.AddAsync(obj);
            await _context.SaveChangesAsync();

            return await ObterPorId(obj.Id);
        }

        public async Task<Usuario?> Atualizar(Usuario obj)
        {
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == obj.Id);

            _context.Entry(usuario).CurrentValues.SetValues(obj);
            _context.Update<Usuario>(usuario);

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task Deletar(Usuario obj)
        {
            // Delete físico, deleta primeiro no contexto e depois no banco.
            _context.Entry(obj).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Usuario>> Obter()
        {
            return await _context.Usuarios.AsNoTracking()
                                          .OrderBy(u => u.Id)
                                          .ToListAsync();
        }

        public async Task<Usuario?> ObterPorId(long id)
        {
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

            return usuario;
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking()
                                          .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}