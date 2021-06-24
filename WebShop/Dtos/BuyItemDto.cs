using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Dtos
{
    public class BuyItemDto
    {
        public string ItemName { get; set; }

        public int ItemQuantity { get; set; }

        public decimal? ItemPrice { get; set; }
    }
}
