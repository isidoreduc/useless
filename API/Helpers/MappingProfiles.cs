using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(x => x.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
                .ForMember(x => x.ProductType, o => o.MapFrom(p => p.ProductType.Name))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<BasketItemDTO, BasketItem>();
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<AddressDTO, Core.Entities.OrderAggregate.Address>();

        }
    }
}