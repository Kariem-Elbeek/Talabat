using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams specParams) : base(
            P =>
                (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search))
                &&
                (!specParams.BrandId.HasValue || P.ProductBrandId == specParams.BrandId)
                &&
                (!specParams.TypeId.HasValue || P.ProductTypeId == specParams.TypeId))
        {

            //if (brandId != null && typeId != null)
            //    Criteria = P => (P.ProductBrandId == brandId) && (P.ProductTypeId == typeId);
            //else if (brandId != null)
            //    Criteria = P => P.ProductBrandId == brandId; 
            //else if (typeId != null)
            //    Criteria = P => P.ProductTypeId == typeId; 

            //Criteria = P => 
            //               (!brandId.HasValue || P.ProductBrandId == brandId)
            //               &&
            //               (!typeId.HasValue || P.ProductTypeId == typeId);

            Take = specParams.PageSize;
            Skip = (specParams.PageIndex - 1) * specParams.PageSize;

            ApplyPagination(Skip, Take);

            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            if (specParams.Sort != null)
            {
                switch (specParams.Sort)
                {
                    case "name":
                        OrderBy = P => P.Name;
                        break;

                    case "priceAsc":
                        OrderBy = P => P.Price;
                        break;

                    case "priceDesc":
                        OrderByDesc = P => P.Price;
                        break;
                    default:
                        OrderBy = P => P.Name;
                        break;
                }
            }

        }
        public ProductSpecification(int id) : base(P => P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
            //Criteria = P => P.Id == id;
        }
    }
}
