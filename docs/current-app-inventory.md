# Current App Inventory

This inventory describes the current ASP.NET Core MVC application structure and behavior.

## C# model files

- `OnlineShop/Models/Category.cs`
- `OnlineShop/Models/Order.cs`
- `OnlineShop/Models/Product.cs`

## Model details

### `OnlineShop/Models/Product.cs`

| Field | Type | Notes |
| --- | --- | --- |
| `Id` | `int` | Primary identifier. |
| `Name` | `string` | Product display name. Defaults to `string.Empty`. |
| `Sku` | `string` | Stock keeping unit. Defaults to `string.Empty`. |
| `Price` | `decimal` | Product price. |
| `StockQuantity` | `int` | Current inventory count. |
| `CategoryId` | `int` | Foreign key to the related category. |
| `Category` | `Category?` | Optional navigation property for the related category. |

### `OnlineShop/Models/Category.cs`

| Field | Type | Notes |
| --- | --- | --- |
| `Id` | `int` | Primary identifier. |
| `Name` | `string` | Category display name. Defaults to `string.Empty`. |
| `Description` | `string` | Category description. Defaults to `string.Empty`. |
| `Products` | `ICollection<Product>` | Navigation collection of products in the category. Defaults to an empty `List<Product>`. |

### `OnlineShop/Models/Order.cs`

| Field | Type | Notes |
| --- | --- | --- |
| `Id` | `int` | Primary identifier. |
| `CustomerName` | `string` | Customer display name. Defaults to `string.Empty`. |
| `TotalAmount` | `decimal` | Total order amount. |
| `CreatedAtUtc` | `DateTime` | Order creation timestamp in UTC. |
| `Status` | `OrderStatus` | Current order status. |

#### `OrderStatus` enum values

| Value | Numeric value |
| --- | ---: |
| `Pending` | `0` |
| `Completed` | `1` |
| `Cancelled` | `2` |

## Controller files

- `OnlineShop/Controllers/CategoriesController.cs`
- `OnlineShop/Controllers/DashboardController.cs`
- `OnlineShop/Controllers/OrdersController.cs`
- `OnlineShop/Controllers/ProductsController.cs`

## Controller routes

The app uses the conventional MVC route pattern `{controller=Dashboard}/{action=Index}/{id?}`. The routes below are provided by the current controller actions.

### `ProductsController`

| HTTP method | Route | Action | Behavior |
| --- | --- | --- | --- |
| `GET` | `/Products` | `Index` | Lists products including their categories, ordered by product name. |
| `GET` | `/Products/Index` | `Index` | Lists products including their categories, ordered by product name. |
| `GET` | `/Products/Create` | `Create` | Shows the product creation form after loading category options. |
| `POST` | `/Products/Create` | `Create` | Creates a product when model state is valid; otherwise redisplays the form with category options. |

### `CategoriesController`

| HTTP method | Route | Action | Behavior |
| --- | --- | --- | --- |
| `GET` | `/Categories` | `Index` | Lists categories ordered by name. |
| `GET` | `/Categories/Index` | `Index` | Lists categories ordered by name. |
| `GET` | `/Categories/Create` | `Create` | Shows the category creation form. |
| `POST` | `/Categories/Create` | `Create` | Creates a category when model state is valid; otherwise redisplays the form. |

### `OrdersController`

| HTTP method | Route | Action | Behavior |
| --- | --- | --- | --- |
| `GET` | `/Orders` | `Index` | Lists orders ordered by creation time descending. |
| `GET` | `/Orders/Index` | `Index` | Lists orders ordered by creation time descending. |
| `GET` | `/Orders/Create` | `Create` | Shows the order creation form with `CreatedAtUtc` initialized to the current UTC time. |
| `POST` | `/Orders/Create` | `Create` | Creates an order with `CreatedAtUtc` reset to the current UTC time when model state is valid; otherwise redisplays the form. |

### `DashboardController`

| HTTP method | Route | Action | Behavior |
| --- | --- | --- | --- |
| `GET` | `/` | `Index` | Default route; shows dashboard totals and recent orders. |
| `GET` | `/Dashboard` | `Index` | Shows dashboard totals and recent orders. |
| `GET` | `/Dashboard/Index` | `Index` | Shows dashboard totals and recent orders. |

## Razor view files

- `OnlineShop/Views/Categories/Create.cshtml`
- `OnlineShop/Views/Categories/Index.cshtml`
- `OnlineShop/Views/Dashboard/Index.cshtml`
- `OnlineShop/Views/Orders/Create.cshtml`
- `OnlineShop/Views/Orders/Index.cshtml`
- `OnlineShop/Views/Products/Create.cshtml`
- `OnlineShop/Views/Products/Index.cshtml`
- `OnlineShop/Views/Shared/_Layout.cshtml`
- `OnlineShop/Views/_ViewImports.cshtml`
- `OnlineShop/Views/_ViewStart.cshtml`

## Seed data in `Program.cs`

When the database is created and no categories exist, the app seeds:

- Categories:
  - `Pantry` with description `Shelf-stable grocery items`.
  - `Produce` with description `Fresh fruits and vegetables`.
- Products:
  - `Olive Oil` with SKU `PAN-001`, price `14.99`, stock quantity `34`, in `Pantry`.
  - `Brown Rice` with SKU `PAN-002`, price `7.49`, stock quantity `50`, in `Pantry`.
  - `Apples` with SKU `PRO-001`, price `3.25`, stock quantity `80`, in `Produce`.
- One order:
  - Customer name `Walk-in Customer`.
  - `CreatedAtUtc` set to the current UTC time at seeding.
  - Total amount `22.48`.
  - Status `Completed`.
