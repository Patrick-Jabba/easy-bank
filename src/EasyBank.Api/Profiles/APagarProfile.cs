using AutoMapper;
using EasyBank.Api.Domain.Models;
using EasyBank.Api.DTO.APagar;

namespace EasyBank.Api.Profiles
{
    public class APagarProfile : Profile
    {
        public APagarProfile()
        {
            CreateMap<APagarRequestDTO, APagar>().ReverseMap();
            CreateMap<APagarResponseDTO, APagar>().ReverseMap();
        }
    }
}