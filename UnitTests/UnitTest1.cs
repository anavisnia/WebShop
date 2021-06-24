using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Controllers;
using WebShop.Entities;
using WebShop.Interfaces;
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

            repository.Setup(r => r.GetAll()).ReturnsAsync(new List<Product>()
            {
                new Product()
                {
                    Price = 3.00M
                }
            });

            var productController = new ProductController(repository.Object, discountService);

            var result = await productController.GetAll();

            //result.Should().NotBeEmpty();
            result.First().Price.Should().Be(2.7M);
        }
    }
}
