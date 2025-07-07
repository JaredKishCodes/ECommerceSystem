
using AutoMapper;
using ProductService.Application.DTOs.Product;



namespace ProductService.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductService.Domain.Entities.Product, ProductDto>().ReverseMap();
            CreateMap<ProductService.Domain.Entities.Product, ProductResponse>().ReverseMap();
        }
    }
}
