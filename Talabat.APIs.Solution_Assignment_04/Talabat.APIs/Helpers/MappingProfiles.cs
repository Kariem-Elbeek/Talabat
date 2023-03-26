using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Entities.OrderAggregate;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s=> s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s=> s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
            CreateMap<AddressDto, Talabat.Core.Entities.Identity.Address> ().ReverseMap();
            CreateMap<AddressDto, Talabat.Core.Entities.OrderAggregate.Address>().ReverseMap();
        }
        
    }
}
