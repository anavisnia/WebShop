using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data;
using WebShop.Dtos;
using WebShop.Interfaces;
using WebShop.Entities;

namespace WebShop.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ShopRepository(DataContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ShopDto>> GetAll()
        {
            var entities = await _context.Shops.ToListAsync();

            return _mapper.Map<List<ShopDto>>(entities);
        }

        public async Task Add(ShopDto shopDto)
        {
            var shop = _mapper.Map<Shop>(shopDto);

            _context.Add(shop);

            await _context.SaveChangesAsync();
        }

        public ShopDto GetById(int id)
        {
            var shop = _context.Set<Shop>().FirstOrDefault(s => s.Id == id);

            if (shop == null)
            {
                throw new ArgumentNullException();
            }

            return _mapper.Map<ShopDto>(shop);
        }

        public async Task Delete(int id)
        {
            var shop = _context.Set<Shop>().FirstOrDefault(s => s.Id == id);

            if (shop != null)
            {
                _context.Remove(shop);
            }

            await _context.SaveChangesAsync();
        }
    }
}
