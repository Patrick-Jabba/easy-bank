using EasyBank.Api.Data;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyBank.Api.Domain.Repository.Classes
{
    public class NaturezaDeLancamentoRepository : INaturezaDeLancamentoRepository
    {
        private readonly ApplicationContext _context;

        public NaturezaDeLancamentoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<NaturezaDeLancamento> Adicionar(NaturezaDeLancamento obj)
        {
            await _context.NaturezaDeLancamentos.AddAsync(obj);
            await _context.SaveChangesAsync();

            return await ObterPorId(obj.Id);
        }

        public async Task<NaturezaDeLancamento?> Atualizar(NaturezaDeLancamento obj)
        {
            NaturezaDeLancamento? naturezaDeLancamento = await _context.NaturezaDeLancamentos.FirstOrDefaultAsync(n => n.Id == obj.Id);

            _context.Entry(naturezaDeLancamento).CurrentValues.SetValues(obj);
            _context.Update<NaturezaDeLancamento>(naturezaDeLancamento);

            await _context.SaveChangesAsync();

            return naturezaDeLancamento;
        }

        public async Task Deletar(NaturezaDeLancamento obj)
        {
            // Delete lógico apenas alterando a data de inativação
            obj.DataInativacao = DateTime.Now;

            await Atualizar(obj);
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> Obter()
        {
            return await _context.NaturezaDeLancamentos.AsNoTracking()
                                                       .OrderBy(u => u.Id)
                                                       .ToListAsync();
        }

        public async Task<IEnumerable<NaturezaDeLancamento>> ObterPeloIdUsuario(long idUsuario)
        {
            return await _context.NaturezaDeLancamentos.AsNoTracking()
                                                       .Where(n => n.IdUsuario == idUsuario)
                                                       .OrderBy(n => n.Id)
                                                       .ToListAsync();
        }

        public async Task<NaturezaDeLancamento?> ObterPorId(long id)
        {
            NaturezaDeLancamento? naturezaDeLancamento = await _context.NaturezaDeLancamentos.FirstOrDefaultAsync(n => n.Id == id);

            return naturezaDeLancamento;
        }
    }
}