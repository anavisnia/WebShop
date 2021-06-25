using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Dtos;
using WebShop.Entities;

namespace WebShop.Services
{
    public class DiscountService
    {
        public decimal GetDiscountedPrice(ProductDto product, int quantity)
        {
            var fullPrice = (decimal)product.Price * quantity;
            if(quantity >= 5)
            {
                return fullPrice / 100.0M * 80;
            }
            else
            {
                return fullPrice / 100.0M * 90;
            }
        }
    }
}
