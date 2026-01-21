# Mini Grocery Order System

## Project Overview

This project is a .NET Web API for a Mini Grocery Order System. It provides basic functionalities for viewing products and placing orders. The system is designed to ensure transactional integrity, where placing an order correctly updates the product stock.

## Tech Stack

*   **.NET 10**: Framework for building the Web API.
*   **ASP.NET Core**: For creating web APIs.
*   **Entity Framework Core**: ORM for database interaction.
*   **SQL Server**: Relational database for storing product and order data.
*   **Swagger**: For API documentation and testing.

## Project Structure

The project follows a standard .NET Web API structure, separating concerns into different layers.

```
/
├── Controllers/        
│   ├── ProductsController.cs
│   └── OrdersController.cs
├── Data/              
│   └── AppDbContext.cs
├── Migrations/        
├── Models/            
│   ├── Product.cs
│   ├── Order.cs
│   └── PlaceOrderRequest.cs
├── Repositories/      
│   ├── IProductRepository.cs
│   ├── ProductRepository.cs
│   ├── IOrderRepository.cs
│   └── OrderRepository.cs
├── Services/          
│   ├── IOrderService.cs
│   └── OrderService.cs
├── appsettings.json   
├── Program.cs         
└── GroceryOrderSystem.csproj 
```

## API Endpoints

The following API endpoints are available:

### Products

*   **`GET /Products`**
    *   **Description**: Retrieves a list of all available products and their current stock levels.
    *   **Response**: `200 OK`
        ```json
        [
          {
            "id": 1,
            "name": "Apple",
            "price": 1.5,
            "stock": 100
          },
          {
            "id": 2,
            "name": "Banana",
            "price": 0.75,
            "stock": 150
          }
        ]
        ```

### Orders

*   **`POST /Orders`**
    *   **Description**: Places a new order for a product. This will decrease the stock of the ordered product.
    *   **Request Body**:
        ```json
        {
          "productId": 1,
          "quantity": 2
        }
        ```
    *   **Success Response**: `201 Created`
        ```json
        {
          "id": 1,
          "productId": 1,
          "quantity": 2,
          "totalPrice": 3.0,
          "createdAt": "2026-01-21T14:30:00Z"
        }
        ```
    *   **Error Response**: `400 Bad Request` if the product is not found or has insufficient stock.

## Business Logic Location

The core business logic for this application is centralized in the **`Services`** layer. Specifically, the `OrderService.cs` file contains the logic for placing an order, which includes stock validation and reduction.

## Transaction Handling

To ensure data consistency, the `OrderService.PlaceOrderAsync` method wraps the order placement logic in a database transaction using `AppDbContext.Database.BeginTransactionAsync()`. If any part of the process fails (e.g., insufficient stock), the transaction is rolled back, ensuring that no partial data changes are saved to the database.

## Setup Instructions

1.  **Clone the repository**:
    ```sh
    git clone <your-repository-url>
    cd GroceryOrderSystem
    ```

2.  **Configure Database Connection**:
    *   Open the `appsettings.Development.json` file.
    *   Update the `DefaultConnection` string to point to your local SQL Server instance.

3.  **Apply Database Migrations**:
    *   Run the following command in your terminal to create the database and apply the initial schema.
    ```sh
    dotnet ef database update
    ```

## How to Run

1.  Ensure you have the .NET 10 SDK installed.
2.  Run the application using the following command:
    ```sh
    dotnet run
    ```
3.  The API will be available at `https://localhost:<port>` and `http://localhost:<port>`.
4.  Navigate to `https://localhost:<port>/swagger` in your browser to access the Swagger UI for interactive API documentation and testing.

## Testing Instructions

You can test the transactional logic by following these steps:

1.  Send a **`GET /Products`** request to see the initial stock of products.
2.  Send a **`POST /Orders`** request with a valid `productId` and `quantity`.
3.  Send another **`GET /Products`** request to verify that the stock for the ordered product has been correctly decremented.


