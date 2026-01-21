using GroceryOrderSystem.Models;
using GroceryOrderSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroceryOrderSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PlaceOrderRequest request)
    {
        try
        {
            var order = await _orderService.PlaceOrderAsync(request.ProductId, request.Quantity);
            return CreatedAtAction(nameof(Post), new { id = order.Id }, order);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
