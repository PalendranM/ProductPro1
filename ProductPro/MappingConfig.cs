using AutoMapper;
using ProductPro.Models;
using ProductPro.Models.Dto;

namespace ProductPro
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            //CreateMap<Product, ProductDto>();
            //CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
        }
    }
}
