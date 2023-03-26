using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        public int ProductBrandId { get; set; } // int : not Allow Null
        public ProductBrand ProductBrand { get; set; } // Navigitional Property [1]

        public int ProductTypeId { get; set; } // int : not Allow Null
        public ProductType ProductType { get; set; } // Navigitional Property [1]
    }
}
