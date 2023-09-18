using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.NaturezaDeLancamento;
using EasyBank.Api.Exceptions;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class NaturezaDeLancamentoService : INaturezaDeLancamentoService
    {
        private readonly INaturezaDeLancamentoRepository _naturezaDeLancamentoRepository;

        private readonly IMapper _mapper;

        public NaturezaDeLancamentoService(INaturezaDeLancamentoRepository naturezaDeLancamentoRepository, IMapper mapper)
        {
            _naturezaDeLancamentoRepository = naturezaDeLancamentoRepository;
            _mapper = mapper;
        }
        public async Task<NaturezaDeLancamentoResponseDTO> Adicionar(NaturezaDeLancamentoRequestDTO entidade, long idUsuario)
        {
            var naturezaDeLancamento = _mapper.Map<NaturezaDeLancamento>(entidade);

            naturezaDeLancamento.DataCadastro = DateTime.Now;
            naturezaDeLancamento.IdUsuario = idUsuario;

            naturezaDeLancamento = await _naturezaDeLancamentoRepository.Adicionar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseDTO>(naturezaDeLancamento);
        }

        public async Task<NaturezaDeLancamentoResponseDTO> Atualizar(long id, NaturezaDeLancamentoRequestDTO entidade, long idUsuario)
        {
            NaturezaDeLancamento? naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            _mapper.Map(entidade, naturezaDeLancamento);

            naturezaDeLancamento = await _naturezaDeLancamentoRepository.Atualizar(naturezaDeLancamento);

            return _mapper.Map<NaturezaDeLancamentoResponseDTO>(naturezaDeLancamento);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _naturezaDeLancamentoRepository.Deletar(naturezaDeLancamento);
        }

        public async Task<IEnumerable<NaturezaDeLancamentoResponseDTO>> ObterPeloIdUsuario(long idUsuario)
        {
            var naturezasDeLancamento = await _naturezaDeLancamentoRepository.ObterPeloIdUsuario(idUsuario);

            return naturezasDeLancamento.Select(n => _mapper.Map<NaturezaDeLancamentoResponseDTO>(n));
        }

         public async Task<IEnumerable<NaturezaDeLancamentoResponseDTO>> Obter()
        {
            var naturezasDeLancamento = await _naturezaDeLancamentoRepository.Obter();

            return naturezasDeLancamento.Select(n => _mapper.Map<NaturezaDeLancamentoResponseDTO>(n));
        }

        public async Task<NaturezaDeLancamentoResponseDTO> ObterPorId(long id, long idUsuario)
        {
            NaturezaDeLancamento naturezaDeLancamento = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<NaturezaDeLancamentoResponseDTO>(naturezaDeLancamento);
        }

        private async Task<NaturezaDeLancamento> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var naturezaDeLancamento = await _naturezaDeLancamentoRepository.ObterPorId(id);

            if(naturezaDeLancamento is null || naturezaDeLancamento.IdUsuario != idUsuario)
            {
                throw new NotFoundException($"Não foi encontrada nenhuma natureza de lançamento pelo id {id}");
            }

            return naturezaDeLancamento;
        }

        public async Task<IEnumerable<NaturezaDeLancamentoResponseDTO>> Obter(long idUsuario)
        {
            var naturezasDelancamento = await _naturezaDeLancamentoRepository.ObterPeloIdUsuario(idUsuario);
            return naturezasDelancamento.Select(natureza => _mapper.Map<NaturezaDeLancamentoResponseDTO>(natureza));
        }
    }
}