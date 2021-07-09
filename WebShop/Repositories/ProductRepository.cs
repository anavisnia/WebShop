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

        public async Task Add(ProductDto productDto)
        {
            var entity = _mapper.Map<Product>(productDto);

            _context.Add(entity);

            await _context.SaveChangesAsync();
        }

        public Product GetByName(string name)
        {
            return _context.Products.FirstOrDefault(s => s.Name == name);
        }
    }
}
