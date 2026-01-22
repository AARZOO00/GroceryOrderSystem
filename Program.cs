using GroceryOrderSystem.Data;
using GroceryOrderSystem.Models;
using GroceryOrderSystem.Repositories;
using GroceryOrderSystem.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ REGISTER DB CONTEXT (IMPORTANT)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// ✅ Seed Method
void SeedData(AppDbContext context)
{
    if (!context.Products.Any())
    {
        context.Products.AddRange(
            new Product { Name = "Apple", Price = 1.50m, Stock = 100 },
            new Product { Name = "Banana", Price = 1.75m, Stock = 150 },
            new Product { Name = "Mango", Price = 1.50m, Stock = 100 },
            new Product { Name = "Cucumber", Price = 2.75m, Stock = 150 },
            new Product { Name = "Date", Price = 3.50m, Stock = 200 },
            new Product { Name = "Orange", Price = 1.50m, Stock = 300},
            new Product { Name = "Pineapple", Price = 1.50m, Stock = 320 },
            new Product { Name = "Grapes", Price = 2.75m, Stock = 200},
            new Product { Name = "Strawberry", Price = 4.50m, Stock = 180},
            new Product { Name = "Blueberry", Price = 5.50m, Stock = 200}
        );
        context.SaveChanges();
    }
}
