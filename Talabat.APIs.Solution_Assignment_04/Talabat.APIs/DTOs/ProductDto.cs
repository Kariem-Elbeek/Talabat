using Talabat.Core.Entities;

namespace Talabat.APIs.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public int ProductBrandId { get; set; } // int : not Allow Null
        public string ProductBrand { get; set; } 
        public int ProductTypeId { get; set; } // int : not Allow Null
        public string ProductType { get; set; }

    }
}
