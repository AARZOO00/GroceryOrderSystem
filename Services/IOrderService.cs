using GroceryOrderSystem.Models;

namespace GroceryOrderSystem.Services;

public interface IOrderService
{
    Task<Order> PlaceOrderAsync(int productId, int quantity);
}
