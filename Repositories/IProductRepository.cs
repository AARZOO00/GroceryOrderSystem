using GroceryOrderSystem.Models;

namespace GroceryOrderSystem.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task UpdateAsync(Product product);
}
