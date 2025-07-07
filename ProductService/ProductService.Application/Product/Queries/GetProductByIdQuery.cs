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
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse>;
    public class GetProductByIdQueryHandler(IProductService _productService) : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
          return await  _productService.GetProductByIdAsync(request.Id);
                
        }
    }
}
