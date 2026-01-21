using GroceryOrderSystem.Models;

namespace GroceryOrderSystem.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);
}
