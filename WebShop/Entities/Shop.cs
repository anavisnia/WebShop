using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Entities.Base;

namespace WebShop.Entities
{
    public class Shop : Entity
    {
        public List<Product> Products { get; set; }
    }
}
