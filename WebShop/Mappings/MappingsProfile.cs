using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
