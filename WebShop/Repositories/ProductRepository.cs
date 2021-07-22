using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Dtos;
using WebShop.Entities;
using WebShop.Interfaces;

namespace WebShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var entities = await _context.Products.Include(s => s.Shop).ToListAsync();

            return _mapper.Map<List<ProductDto>>(entities);
        }

        //public Product GetEntity(ProductDto productdto)

        //{
        //    var product = _mapper.Map<Product>(productdto);

        //    var entity = _context.Products.FirstOrDefault(p => p == product);

        //    return entity;
        //}


        public Product GetByName(string name)
        {
            return _context.Products.FirstOrDefault(s => s.Name == name);
        }

        public async Task GetQuantytiAfterBuy(Product product, int quantity)
        {
            var updatedQuantity = product.Quantity - quantity;

            product.Quantity = updatedQuantity;

            await _context.SaveChangesAsync();
        }

        public async Task Upsert(ProductDto productDto)
        {
            var entity = _mapper.Map<Product>(productDto);
            // ???
            if (entity.Id == productDto.Id)
            {
                _context.Update(entity);
            }
            else
            {
                _context.Add(entity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = _context.Set<Product>().FirstOrDefault(p => p.Id == id);


            if (product != null)
            {
                _context.Remove(product);
            }

            await _context.SaveChangesAsync();
        }

        //public async Task DeleteObj(ProductDto productDto)
        //{
        //    //var productdto = _context.Set<ProductDto>().FirstOrDefault(p => p.Id == id);

        //    var product = _mapper.Map<Product>(productDto);

        //    if (product != null)
        //    {
        //        _context.Remove(product);
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}
