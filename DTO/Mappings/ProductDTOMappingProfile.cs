using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using catalog.Models;

namespace catalog.DTO.Mappings
{
    public class ProductDTOMappingProfile : Profile
    {
        public ProductDTOMappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product,ProductDTOUpadateRequest>().ReverseMap();
            CreateMap<Product,ProductDtoUpdateResponse>().ReverseMap();
        }
    }
}