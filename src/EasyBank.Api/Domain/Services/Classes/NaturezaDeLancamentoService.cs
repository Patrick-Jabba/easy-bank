using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyBank.Api.Domain.Repository.Classes;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.NaturezaDeLancamento;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class NaturezaDeLancamentoService : IService<NaturezaDeLancamentoRequestDto, NaturezaDeLancamentoResponseDto, long>
    {
        private readonly NaturezaDeLancamentoRepository _repository;

        public NaturezaDeLancamentoService(NaturezaDeLancamentoRepository repository)
        {
            _repository = repository;
        }
        public Task<NaturezaDeLancamentoResponseDto> Adicionar(NaturezaDeLancamentoRequestDto entidade, long idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<NaturezaDeLancamentoResponseDto> Atualizar(long id, NaturezaDeLancamentoRequestDto entidade, long idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task Inativar(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NaturezaDeLancamentoResponseDto>> Obter()
        {
            throw new NotImplementedException();
        }

        public Task<NaturezaDeLancamentoResponseDto> ObterPorId(long id)
        {
            throw new NotImplementedException();
        }
    }
}