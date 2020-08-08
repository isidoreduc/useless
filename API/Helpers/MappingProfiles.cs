using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
                .ForMember(x => x.ProductType, o => o.MapFrom(p => p.ProductType.Name));
        }
    }
}