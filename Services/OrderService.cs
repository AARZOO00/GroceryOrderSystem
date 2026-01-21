using GroceryOrderSystem.Data;
using GroceryOrderSystem.Models;
using GroceryOrderSystem.Repositories;

namespace GroceryOrderSystem.Services;

public class OrderService : IOrderService
{
    private readonly IProductRepository _productRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly AppDbContext _context;

    public OrderService(IProductRepository productRepository, IOrderRepository orderRepository, AppDbContext context)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _context = context;
    }

    public async Task<Order> PlaceOrderAsync(int productId, int quantity)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.Stock < quantity)
            {
                throw new Exception("Insufficient stock");
            }

            product.Stock -= quantity;
            await _productRepository.UpdateAsync(product);

            var order = new Order
            {
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = product.Price * quantity,
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(order);

            await transaction.CommitAsync();

            return order;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
