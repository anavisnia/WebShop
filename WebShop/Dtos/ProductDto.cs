using WebShop.Dtos.Base;

namespace WebShop.Dtos
{
    public class ProductDto : DtoObject
    {
        public int? Id { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
    }
}
