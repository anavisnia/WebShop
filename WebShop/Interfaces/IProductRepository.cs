using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Dtos;
using WebShop.Entities;

namespace WebShop.Interfaces
{
    public interface IProductRepository
    {
        Task Add(ProductDto product);

        Task<List<ProductDto>> GetAll();

        //Product GetEntity(ProductDto productdto);

        Product GetByName(string name);

        Task Delete(int id);

        //Task DeleteObj(ProductDto productDto);
    }
}