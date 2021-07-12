using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Dtos;
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
        private readonly IMapper _mapper;

        public ProductController(IProductRepository repository, DiscountService discountService, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAll()
        {
            var entities = await _repository.GetAll();

            return entities;
        }

        [HttpGet("/GetAllWithDiscount")]
        public async Task<List<ProductDto>> GetAllWithDiscount()
        {
            var items = await _repository.GetAll();

            items.ForEach(i => i.Price = _discountService.GetDiscountedPrice(i, i.Quantity));

            return _mapper.Map<List<ProductDto>>(items);
        }

        [HttpPost("/Buy")]
        public decimal Buy(string itemName, int quantity)
        {
            var item = _repository.GetByName(itemName);

            if (item == null)
            {
                return (-1);
            }

            if (quantity <= item.Quantity)
            {
                var itemConvert = _mapper.Map<ProductDto>(item);
                return _discountService.GetDiscountedPrice(itemConvert, item.Quantity);
            }
            else
            {
                return (-1);
            }
        }

        [HttpPost]
        public async Task Upsert(ProductDto dto)
        {
            await _repository.Upsert(dto);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        //[HttpDelete("{obj}")]
        //public async Task Delete(ProductDto productDto)
        //{
        //    await _repository.DeleteObj(productDto);
        //}

    }
}
