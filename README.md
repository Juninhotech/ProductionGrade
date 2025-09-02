# Product Grade Api

## 1. Introduction
This project is an ASP.NET Core Web API for a simple e-commerce platform. It provides functionality to manage products, customers, and orders.

---

## 2. Project Structure
This solution follows **Clean Architecture** principles with clear separation of concerns:

- **Core Layer**: Domain entities, interfaces, and business exceptions
- **Application Layer**: Business logic, services, DTOs, and mappings  
- **Infrastructure Layer**: Data access, repositories, and external concerns
- **API Layer**: Controllers, middleware, and presentation concerns
---

## 3. Setup Instructions

### Prerequisites
- .NET 8
- SQL Server (LocalDB or Express recommended)
- Visual Studio 2022 or VS Code

### Steps
1. Clone the repository:
   ```bash
  https://github.com/Juninhotech/ProductionGrade.git
   ```
2. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ProductGrade;Trusted_Connection=True;"
   }
   ```
3. Run migrations and update database:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

---

## 4. Middleware
Middleware is used to handle cross-cutting concerns like authentication, error handling, and logging.  
In this project:
- **Swagger** is enabled by default for API documentation
- **Exception Handling Middleware** ensures consistent error responses

---

## 5. API Endpoints

### Products
- `GET /api/v1/products` - Get all products  
- `GET /api/v1/products/{id}` - Get product by ID  
- `POST /api/v1/products` - Create new product  
- `PUT /api/v1/products/{id}` - Update product  
- `DELETE /api/v1/products/{id}` - Delete product  

### Orders
- `POST /api/v1/orders` - Create new order  
- `GET /api/v1/orders/{id}` - Get order by ID  
- `GET /api/v1/orders/customer/{email}` - Get orders by customer email  

---

## 6. Example API Calls

**Create Product**
```http
POST /api/v1/products
Content-Type: application/json

{
  "name": "Gaming Monitor",
  "description": "27-inch 4K gaming monitor",
  "price": 399.99,
  "stockQuantity": 15
}
```

**Create Order**
```http
POST /api/v1/orders
Content-Type: application/json

{
  "customerEmail": "customer@example.com",
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 2, "quantity": 1 }
  ]
}
```

---

## 7. Assumptions
- **Stock Management**: Negative stock quantities are not allowed  
- **Order Processing**: Orders are immediately confirmed if stock is available  
- **Customer Management**: Simple email-based customer identification (no full user management)  
- **Pricing**: Product prices are taken at order time (no dynamic pricing)  
- **Database**: MSSQL for simplicity, but designed to work with any relational database  
