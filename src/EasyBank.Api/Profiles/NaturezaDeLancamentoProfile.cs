using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.DTO.NaturezaDeLancamento;

namespace EasyBank.Api.Profiles
{
    public class NaturezaDeLancamentoProfile : Profile
    {
        public NaturezaDeLancamentoProfile()
        {
            CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoRequestDTO>().ReverseMap();
            CreateMap<NaturezaDeLancamento, NaturezaDeLancamentoResponseDTO>().ReverseMap();
        }
    }
}