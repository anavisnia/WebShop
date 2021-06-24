using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Dtos;
using WebShop.Entities;
using WebShop.Interfaces;
using WebShop.Services;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly DiscountService _discountService;

        public ProductController(IProductRepository repository, DiscountService discountService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
        }

        [HttpGet]
        public async Task<List<Product>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("/GetAllWithDiscount")]
        public async Task<List<Product>> GetAllWithDiscount()
        {
            var items = await _repository.GetAll();

            items.ForEach(i => i.Price = _discountService.GetDiscountedPrice(i, i.Quantity));

            return items;
        }

        [HttpPost]
        public async Task Create(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException();
            }

            await _repository.Add(product);
        }

        [HttpPost("/Buy")]
        public decimal Buy(BuyItemDto itemDto, int quantity)
        {
            var item = _repository.GetByName(itemDto.ItemName);

            if(quantity <= item.Quantity)
            {
                return _discountService.GetDiscountedPrice(item, itemDto.ItemQuantity);
            }
            else
            {
                throw new ArgumentNullException("There is no that many items. Please, try again.");
            }
        }

    }
}
