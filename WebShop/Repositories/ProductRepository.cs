using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Entities;
using WebShop.Interfaces;

namespace WebShop.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task Add(Product product)
        {
            _context.Add(product);

            await _context.SaveChangesAsync();
        }

        public Product GetByName(string name)
        {
            return _context.Products.FirstOrDefault(s => s.Name == name);
        }
    }
}
