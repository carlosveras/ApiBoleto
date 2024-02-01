using AutoMapper;
using ApiBoleto.Models.Dto;
using ApiBoleto.Models.Entities;

namespace ApiBoleto.Helpers
{
    public class ApiPagProfile : Profile
    {
        public ApiPagProfile()
        {
            CreateMap<BancoAdicionarDto, Banco>().ReverseMap();
            CreateMap<BancoDto, Banco>().ReverseMap();

            CreateMap<BoletoAdicionarDto, Boleto>().ReverseMap();
            CreateMap<BoletoDto, Boleto>().ReverseMap();

        }
    }
}
