﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

            return _mapper.Map<List<ProductDto>>(entities);
        }

        [HttpGet("/GetAllWithDiscount")]
        public async Task<List<ProductDto>> GetAllWithDiscount()
        {
            var items = await _repository.GetAll();

            items.ForEach(i => i.Price = _discountService.GetDiscountedPrice(i, i.Quantity));
            
            return _mapper.Map<List<ProductDto>>(items);
        }

        [HttpPost]
        public async Task Create(ProductDto productDto)
        {
            if(productDto == null)
            {
                throw new ArgumentNullException();
            }

            var product = _mapper.Map<Product>(productDto);

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
