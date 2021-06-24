using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Dtos;
using WebShop.Entities;

namespace WebShop.Interfaces
{
    public interface IShopRepository
    {
        Task Add(ShopDto shopDto);
        Task Delete(int id);
        Task<List<ShopDto>> GetAll();
        ShopDto GetById(int id);
    }
}