using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.DTO.AReceber;

namespace EasyBank.Api.Profiles
{
    public class AReceberProfile : Profile
    {
        public AReceberProfile()
        {
            CreateMap<AReceberRequestDTO, AReceber>().ReverseMap();
            CreateMap<AReceberResponseDTO, AReceber>().ReverseMap();
        }
    }
}