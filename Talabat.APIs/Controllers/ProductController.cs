using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IGenericReopsitory<Product> _genericReopsitory;
        private readonly IGenericReopsitory<ProductBrand> _brandRepo;
        private readonly IGenericReopsitory<ProductType> _typeRepo;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ProductController(IGenericReopsitory<Product> genericReopsitory, IGenericReopsitory<ProductBrand> brandRepo, IGenericReopsitory<ProductType> typeRepo, IMapper mapper, ITokenService tokenService)
        {
            _genericReopsitory = genericReopsitory;
            _brandRepo = brandRepo;
            _typeRepo = typeRepo;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDto>>> GetAllProducts([FromQuery]ProductSpecParams specParams)
        {
            var spec = new ProductSpecification(specParams);
            var products = await _genericReopsitory.GetAllWithSpecAsync(spec);

            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            var countSpec = new ProductWithFiltersForCountSpecifications(specParams);
            var count = await _genericReopsitory.GetCountAsync(countSpec);

            return Ok(new Pagination<ProductDto>(specParams.PageIndex, specParams.PageSize, count, Data));
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        //{            
        //    var product = await _genericReopsitory.GetByIdAsync(id);
        //    return Ok(product);
        //}
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdWithSpecAsync(int id)
        {            
            var spec = new ProductSpecification(id);

            var product = await _genericReopsitory.GetByIdWithSpecAsync(spec);
            if (product == null)
                return NotFound(new ApiResponses(404));
            return Ok(_mapper.Map<Product, ProductDto>(product));
        }
    
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrandsAsync()
        {
            var result = await _brandRepo.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypesAsync()
        {
            var result = await _typeRepo.GetAllAsync();
            return Ok(result);
        }
    
    }
}
