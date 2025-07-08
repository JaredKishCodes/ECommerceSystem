
using AutoMapper;
using ProductService.Application.DTOs.Product;
using ProductEntity = ProductService.Domain.Entities.Product;



namespace ProductService.Application.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductDto>().ReverseMap();
            CreateMap<ProductEntity, ProductResponse>().ReverseMap();
            CreateMap<CreateProductDto, ProductEntity>().ReverseMap();
        }
    }
}
