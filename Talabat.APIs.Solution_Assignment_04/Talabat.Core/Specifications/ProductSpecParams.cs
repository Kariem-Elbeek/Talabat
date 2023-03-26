using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specifications
{
    public class ProductSpecParams
    {
        //string sort, int? brandId, int? typeId
        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int PageIndex { get; set; } = 1;

        private const int MaxPageSize = 10;

        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }

        public bool IsPaginationEnabled { get; set; }

        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


    }
}
