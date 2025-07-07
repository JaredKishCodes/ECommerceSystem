using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Product.Queries
{
    public record GetAllProductsQuery() : IRequest<IEnumerable<ProductResponse>>;
    public class GetAllProductsQueryHandler(IProductService _productService) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
    {
        public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
          return await  _productService.GetAllProductsAsync();
        }
    }
}
