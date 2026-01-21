using GroceryOrderSystem.Models;
using GroceryOrderSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GroceryOrderSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> Get()
    {
        return await _productRepository.GetAllAsync();
    }
}
