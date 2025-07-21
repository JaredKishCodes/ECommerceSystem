using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs;
using OrderService.Application.Order.Commands;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(ISender sender) : ControllerBase
    {
        [HttpPost("{id:guid}")]
        public async Task<ActionResult<ApiResponse<OrderResponse>>> CreateOrder(OrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest(ApiResponse<OrderResponse>.FailResponse("Order data is missing."));
            }

            try
            {
                var command = new AddOrderCommand(orderRequest.ProductId, orderRequest.Quantity);
                
                var result = await sender.Send(command);

                return Ok(ApiResponse<OrderResponse>.SuccessResponse(result, "Order created successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<OrderResponse>.FailResponse(ex.Message));
            }
        }
    }

}
