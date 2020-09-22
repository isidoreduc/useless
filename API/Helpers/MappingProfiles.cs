using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

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
            CreateMap<Core.Entities.Identity.Address, AddressDTO>().ReverseMap();
            CreateMap<BasketItemDTO, BasketItem>();
            CreateMap<CustomerBasketDTO, CustomerBasket>();
            CreateMap<AddressDTO, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(x => x.DeliveryMethod, o => o.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(x => x.DeliveryPrice, o => o.MapFrom(o => o.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(x => x.ProductId, o => o.MapFrom(p => p.ItemOrdered.ProductItemId))
                .ForMember(x => x.ProductName, o => o.MapFrom(p => p.ItemOrdered.ProductName))
                .ForMember(x => x.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());



        }
    }
}