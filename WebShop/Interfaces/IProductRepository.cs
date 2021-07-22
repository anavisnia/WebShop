using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Dtos;
using WebShop.Entities;

namespace WebShop.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetAll();

        //Product GetEntity(ProductDto productdto);
        Task Upsert(ProductDto productDto);

        Product GetByName(string name);

        Task GetQuantytiAfterBuy(Product product, int quantity);

        Task Delete(int id);

        //Task DeleteObj(ProductDto productDto);
    }
}