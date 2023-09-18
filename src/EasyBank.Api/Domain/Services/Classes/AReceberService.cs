using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.AReceber;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class AReceberService : IAReceberService
    {
        private readonly IAReceberRepository _aReceberRepository;

        private readonly IMapper _mapper;

        public AReceberService(IAReceberRepository aReceberRepository, IMapper mapper)
        {
            _aReceberRepository = aReceberRepository;
            _mapper = mapper;
        }
        public async Task<AReceberResponseDTO> Adicionar(AReceberRequestDTO entidade, long idUsuario)
        {
            var aReceber = _mapper.Map<AReceber>(entidade);

            aReceber.DataCadastro = DateTime.Now;
            aReceber.IdUsuario = idUsuario;

            // Ter alguma validação para datas e campos

            aReceber = await _aReceberRepository.Adicionar(aReceber);

            return _mapper.Map<AReceberResponseDTO>(aReceber);
        }

        public async Task<AReceberResponseDTO> Atualizar(long id, AReceberRequestDTO entidade, long idUsuario)
        {
            AReceber? aReceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map(entidade, aReceber);
            contrato.IdUsuario = aReceber.IdUsuario;
            contrato.Id = aReceber.Id;
            contrato.DataCadastro = aReceber.DataCadastro;

            contrato = await _aReceberRepository.Atualizar(contrato);

            return _mapper.Map<AReceberResponseDTO>(contrato);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            AReceber aReceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _aReceberRepository.Deletar(aReceber);
        }

        public async Task<IEnumerable<AReceberResponseDTO>> ObterPeloIdUsuario(long idUsuario)
        {
            var titulosAReceber = await _aReceberRepository.ObterPeloIdUsuario(idUsuario);

            return titulosAReceber.Select(n => _mapper.Map<AReceberResponseDTO>(n));
        }

         public async Task<IEnumerable<AReceberResponseDTO>> Obter()
        {
            var titulosAReceber = await _aReceberRepository.Obter();

            return titulosAReceber.Select(n => _mapper.Map<AReceberResponseDTO>(n));
        }

        public async Task<AReceberResponseDTO> ObterPorId(long id, long idUsuario)
        {
            AReceber aReceber = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<AReceberResponseDTO>(aReceber);
        }

        private async Task<AReceber> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var aReceber = await _aReceberRepository.ObterPorId(id);

            if(aReceber is null || aReceber.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrada nenhum título a pagar pelo id {id}");
            }

            return aReceber;
        }

        public async Task<IEnumerable<AReceberResponseDTO>> Obter(long idUsuario)
        {
            var titulosAReceber = await _aReceberRepository.ObterPeloIdUsuario(idUsuario);
            return titulosAReceber.Select(natureza => _mapper.Map<AReceberResponseDTO>(natureza));
        }
    }
}