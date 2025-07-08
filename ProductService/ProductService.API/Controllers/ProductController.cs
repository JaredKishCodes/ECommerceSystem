using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Product.Commands;
using ProductService.Application.Product.Queries;

namespace ProductService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ISender sender) : ControllerBase
    {
        [HttpPost("reduce-stock/{id}")]
        public async Task<IActionResult> ReduceStock(Guid id, [FromQuery] int quantity)
        {
            try
            {
                var command = new ReduceAvailableStockCommand(id, quantity);
                var result = await sender.Send(command);

                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var command = new GetProductByIdQuery(id);
            var result = await sender.Send(command);

            return Ok(result);

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var command = new GetAllProductsQuery();
            var result = await sender.Send(command);

            return Ok(result);

        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            var command = new AddProductCommand(createProductDto);
            var result = await sender.Send(command);

            return Ok(result);

        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            var command = new UpdateProductCommand(productDto);
            var result = await sender.Send(command);

            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command);

            return Ok(result);

        }



    }
}
