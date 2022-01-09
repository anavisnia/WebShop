using AutoMapper;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Controllers;
using WebShop.Dtos;
using WebShop.Interfaces;
using WebShop.Mappings;
using WebShop.Services;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        private DiscountService discountService;
        private Mock<IProductRepository> repository;
        private MapperConfiguration mockMapper;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            discountService = new DiscountService();
            repository = new Mock<IProductRepository>();
            mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingsProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetAll_ControllerAppliesDiscount()
        {
            SetUp();

            repository.Setup(r => r.GetAll()).ReturnsAsync(new List<ProductDto>()
            {
                new ProductDto()
                {
                    Price = 3.00M,
                    Quantity = 1
                }
            });

            var productController = new ProductController(repository.Object, discountService, mapper);

            var result = await productController.GetAllWithDiscount();

            //result.Should().NotBeEmpty();
            result.First().Price.Should().Be(2.7M);
        }
    }
}
