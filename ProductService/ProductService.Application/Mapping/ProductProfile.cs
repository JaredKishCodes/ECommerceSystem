
using AutoMapper;
using ProductService.Application.DTOs.Product;
using ProductService.Domain.Entities;

namespace ProductService.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
