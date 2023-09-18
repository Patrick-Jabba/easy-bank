using EasyBank.Api.Data;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyBank.Api.Domain.Repository.Classes
{
    public class AReceberRepository : IAReceberRepository
    {
        private readonly ApplicationContext _context;

        public AReceberRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<AReceber> Adicionar(AReceber obj)
        {
            await _context.AReceberContext.AddAsync(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<AReceber?> Atualizar(AReceber obj)
        {
            AReceber? naturezaDeLancamento = await _context.AReceberContext.FirstOrDefaultAsync(n => n.Id == obj.Id);

            if(naturezaDeLancamento != null)
            {
                _context.Entry(naturezaDeLancamento).CurrentValues.SetValues(obj);
                _context.Update(naturezaDeLancamento);
                await _context.SaveChangesAsync();
            }

            return naturezaDeLancamento;
        }

        public async Task Deletar(AReceber obj)
        {
            // Delete lógico apenas alterando a data de inativação
            obj.DataInativacao = DateTime.Now;

            await Atualizar(obj);
        }

        public async Task<IEnumerable<AReceber>> Obter()
        {
            return await _context.AReceberContext.AsNoTracking()
                                                       .OrderBy(u => u.Id)
                                                       .ToListAsync();
        }

        public async Task<IEnumerable<AReceber>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _context.AReceberContext.AsNoTracking()
                                                       .Where(n => n.IdUsuario == idUsuario)
                                                       .OrderBy(n => n.Id)
                                                       .ToListAsync();
        }

        public async Task<AReceber?> ObterPorId(long id)
        {
            AReceber? naturezaDeLancamento = await _context.AReceberContext.FirstOrDefaultAsync(n => n.Id == id);

            return naturezaDeLancamento;
        }
    }
}