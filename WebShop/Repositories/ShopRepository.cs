using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Data;

namespace WebShop.Repositories
{
    public class ShopRepository
    {
        private readonly DataContext _context;

        public ShopRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


    }
}
