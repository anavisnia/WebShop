using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Controllers;
using WebShop.Dtos;
using WebShop.Entities;
using WebShop.Interfaces;
using WebShop.Mappings;
using WebShop.Services;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task GetAll_ControllerAppliesDiscount()
        {
            var discountService = new DiscountService();

            var repository = new Mock<IProductRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingsProfile());
            });

            var mapper = mockMapper.CreateMapper();

            repository.Setup(r => r.GetAll()).ReturnsAsync(new List<ProductDto>()
            {
                new ProductDto()
                {
                    Price = 3.00M
                }
            });

            var productController = new ProductController(repository.Object, discountService, mapper);

            var result = await productController.GetAll();

            //result.Should().NotBeEmpty();
            result.First().Price.Should().Be(2.7M);
        }
    }
}
