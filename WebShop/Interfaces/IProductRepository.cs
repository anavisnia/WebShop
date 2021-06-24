using System.Collections.Generic;
using System.Threading.Tasks;
using WebShop.Entities;

namespace WebShop.Interfaces
{
    public interface IProductRepository
    {
        Task Add(Product product);
        Task<List<Product>> GetAll();
        Product GetByName(string name);
    }
}