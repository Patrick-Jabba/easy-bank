using EasyBank.Api.Data;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyBank.Api.Domain.Repository.Classes
{
    public class APagarRepository : IAPagarRepository
    {
        private readonly ApplicationContext _context;

        public APagarRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<APagar> Adicionar(APagar obj)
        {
            await _context.APagarContext.AddAsync(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<APagar?> Atualizar(APagar obj)
        {
            APagar? naturezaDeLancamento = await _context.APagarContext.FirstOrDefaultAsync(n => n.Id == obj.Id);

            if(naturezaDeLancamento != null)
            {
                _context.Entry(naturezaDeLancamento).CurrentValues.SetValues(obj);
                _context.Update(naturezaDeLancamento);
                await _context.SaveChangesAsync();
            }

            return naturezaDeLancamento;
        }

        public async Task Deletar(APagar obj)
        {
            // Delete lógico apenas alterando a data de inativação
            obj.DataInativacao = DateTime.Now;

            await Atualizar(obj);
        }

        public async Task<IEnumerable<APagar>> Obter()
        {
            return await _context.APagarContext.AsNoTracking()
                                                       .OrderBy(u => u.Id)
                                                       .ToListAsync();
        }

        public async Task<IEnumerable<APagar>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _context.APagarContext.AsNoTracking()
                                                       .Where(n => n.IdUsuario == idUsuario)
                                                       .OrderBy(n => n.Id)
                                                       .ToListAsync();
        }

        public async Task<APagar?> ObterPorId(long id)
        {
            APagar? naturezaDeLancamento = await _context.APagarContext.FirstOrDefaultAsync(n => n.Id == id);

            return naturezaDeLancamento;
        }
    }
}