﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Dtos;
using WebShop.Entities.Base;

namespace WebShop.Entities
{
    public class Product : Entity
    {
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
    }
}
