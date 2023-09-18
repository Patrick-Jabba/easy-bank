using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.Domain.Repository.Interfaces;
using EasyBank.Api.Domain.Services.Interfaces;
using EasyBank.Api.DTO.APagar;

namespace EasyBank.Api.Domain.Services.Classes
{
    public class APagarService : IAPagarService
    {
        private readonly IAPagarRepository _aPagarRepository;

        private readonly IMapper _mapper;

        public APagarService(IAPagarRepository aPagarRepository, IMapper mapper)
        {
            _aPagarRepository = aPagarRepository;
            _mapper = mapper;
        }
        public async Task<APagarResponseDTO> Adicionar(APagarRequestDTO entidade, long idUsuario)
        {
            var aPagar = _mapper.Map<APagar>(entidade);

            aPagar.DataCadastro = DateTime.Now;
            aPagar.IdUsuario = idUsuario;

            // Ter alguma validação para datas e campos

            aPagar = await _aPagarRepository.Adicionar(aPagar);

            return _mapper.Map<APagarResponseDTO>(aPagar);
        }

        public async Task<APagarResponseDTO> Atualizar(long id, APagarRequestDTO entidade, long idUsuario)
        {
            APagar? aPagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            var contrato = _mapper.Map(entidade, aPagar);
            contrato.IdUsuario = aPagar.IdUsuario;
            contrato.Id = aPagar.Id;
            contrato.DataCadastro = aPagar.DataCadastro;

            contrato = await _aPagarRepository.Atualizar(contrato);

            return _mapper.Map<APagarResponseDTO>(contrato);
        }

        public async Task Inativar(long id, long idUsuario)
        {
            APagar aPagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            await _aPagarRepository.Deletar(aPagar);
        }

        public async Task<IEnumerable<APagarResponseDTO>> ObterPeloIdUsuario(long idUsuario)
        {
            var titulosAPagar = await _aPagarRepository.ObterPeloIdUsuario(idUsuario);

            return titulosAPagar.Select(n => _mapper.Map<APagarResponseDTO>(n));
        }

         public async Task<IEnumerable<APagarResponseDTO>> Obter()
        {
            var titulosAPagar = await _aPagarRepository.Obter();

            return titulosAPagar.Select(n => _mapper.Map<APagarResponseDTO>(n));
        }

        public async Task<APagarResponseDTO> ObterPorId(long id, long idUsuario)
        {
            APagar aPagar = await ObterPorIdVinculadoAoIdUsuario(id, idUsuario);

            return _mapper.Map<APagarResponseDTO>(aPagar);
        }

        private async Task<APagar> ObterPorIdVinculadoAoIdUsuario(long id, long idUsuario)
        {
            var aPagar = await _aPagarRepository.ObterPorId(id);

            if(aPagar is null || aPagar.IdUsuario != idUsuario)
            {
                throw new Exception($"Não foi encontrada nenhum título a pagar pelo id {id}");
            }

            return aPagar;
        }

        public async Task<IEnumerable<APagarResponseDTO>> Obter(long idUsuario)
        {
            var titulosAPagar = await _aPagarRepository.ObterPeloIdUsuario(idUsuario);
            return titulosAPagar.Select(natureza => _mapper.Map<APagarResponseDTO>(natureza));
        }
    }
}