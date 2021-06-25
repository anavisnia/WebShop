using AutoMapper;
using WebShop.Dtos;
using WebShop.Entities;

namespace WebShop.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<ShopDto, Shop>().ReverseMap();
        }
    }
}
