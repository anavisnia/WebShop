using AutoMapper;
using WebShop.Dtos;
using WebShop.Entities;

namespace WebShop.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Product, ProductDto>().ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Shop.Name)).ReverseMap();
            CreateMap<ShopDto, Shop>().ReverseMap();
        }
    }
}
