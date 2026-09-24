# Current Application Inventory

This document records the behavior of the ASP.NET Core MVC application before the TypeScript rewrite.

## Models

The current C# model files are:

- `OnlineShop/Models/Category.cs`
- `OnlineShop/Models/Order.cs`
- `OnlineShop/Models/Product.cs`

### Product

`OnlineShop/Models/Product.cs` defines these fields:

| Field | C# type | Notes |
| --- | --- | --- |
| `Id` | `int` | Primary key by Entity Framework Core convention. |
| `Name` | `string` | Defaults to an empty string. |
| `Sku` | `string` | Defaults to an empty string and has a unique database index. |
| `Price` | `decimal` | Stored as `decimal(18,2)`. |
| `StockQuantity` | `int` | Number of units in stock. |
| `CategoryId` | `int` | Required foreign-key value for the category. |
| `Category` | `Category?` | Nullable category navigation property. |

### Category

`OnlineShop/Models/Category.cs` defines these fields:

| Field | C# type | Notes |
| --- | --- | --- |
| `Id` | `int` | Primary key by Entity Framework Core convention. |
| `Name` | `string` | Defaults to an empty string and has a maximum length of 100. |
| `Description` | `string` | Defaults to an empty string. |
| `Products` | `ICollection<Product>` | Collection navigation property, initialized to an empty list. |

### Order

`OnlineShop/Models/Order.cs` defines these fields:

| Field | C# type | Notes |
| --- | --- | --- |
| `Id` | `int` | Primary key by Entity Framework Core convention. |
| `CustomerName` | `string` | Defaults to an empty string. |
| `TotalAmount` | `decimal` | Stored as `decimal(18,2)`. |
| `CreatedAtUtc` | `DateTime` | UTC creation timestamp; the POST action replaces submitted values with the current UTC time. |
| `Status` | `OrderStatus` | Order state enum. |

`OrderStatus` has these values:

- `Pending = 0`
- `Completed = 1`
- `Cancelled = 2`

## Controllers and Routes

The current controller files are:

- `OnlineShop/Controllers/CategoriesController.cs`
- `OnlineShop/Controllers/DashboardController.cs`
- `OnlineShop/Controllers/OrdersController.cs`
- `OnlineShop/Controllers/ProductsController.cs`

Routes use the conventional pattern `{controller=Dashboard}/{action=Index}/{id?}`. An action without an explicit HTTP attribute accepts the framework's default supported methods; the practical UI entry points below use GET. `/` therefore resolves to `DashboardController.Index`.

### ProductsController

| Method | Route | Action behavior |
| --- | --- | --- |
| GET | `/Products` or `/Products/Index` | Lists products alphabetically and includes each product's category. |
| GET | `/Products/Create` | Displays a new-product form and an alphabetically ordered category select list. |
| POST | `/Products/Create` | Validates and creates a product, then redirects to `Index`; redisplays the form and category list when invalid. |

### CategoriesController

| Method | Route | Action behavior |
| --- | --- | --- |
| GET | `/Categories` or `/Categories/Index` | Lists categories alphabetically. |
| GET | `/Categories/Create` | Displays a new-category form. |
| POST | `/Categories/Create` | Validates and creates a category, then redirects to `Index`; redisplays the submitted form when invalid. |

### OrdersController

| Method | Route | Action behavior |
| --- | --- | --- |
| GET | `/Orders` or `/Orders/Index` | Lists orders newest first. |
| GET | `/Orders/Create` | Displays a new-order form initialized with the current UTC time. |
| POST | `/Orders/Create` | Validates and creates an order. It sets `CreatedAtUtc` to the current UTC time before saving, then redirects to `Index`; it redisplays the submitted form when invalid. |

### DashboardController

| Method | Route | Action behavior |
| --- | --- | --- |
| GET | `/`, `/Dashboard`, or `/Dashboard/Index` | Shows product and category totals, pending-order count, total inventory value, and the 10 newest orders. Each displayed customer name is decorated with status, product, and category counts. The implementation intentionally performs repeated queries for performance-practice scenarios. |

## Razor Views

The current Razor view files are:

- `OnlineShop/Views/_ViewImports.cshtml`
- `OnlineShop/Views/_ViewStart.cshtml`
- `OnlineShop/Views/Categories/Create.cshtml`
- `OnlineShop/Views/Categories/Index.cshtml`
- `OnlineShop/Views/Dashboard/Index.cshtml`
- `OnlineShop/Views/Orders/Create.cshtml`
- `OnlineShop/Views/Orders/Index.cshtml`
- `OnlineShop/Views/Products/Create.cshtml`
- `OnlineShop/Views/Products/Index.cshtml`
- `OnlineShop/Views/Shared/_Layout.cshtml`

## Seed Data

At startup, `Program.cs` creates a service scope and invokes `ShopDatabaseInitializer.Initialize()`. The initializer calls `EnsureCreated()` and seeds only when no category exists. If at least one category exists, it adds nothing.

The initial data is:

- Category **Pantry**, described as "Shelf-stable grocery items".
- Category **Produce**, described as "Fresh fruits and vegetables".
- Product **Olive Oil** (`PAN-001`), priced at 14.99 with 34 units, in Pantry.
- Product **Brown Rice** (`PAN-002`), priced at 7.49 with 50 units, in Pantry.
- Product **Apples** (`PRO-001`), priced at 3.25 with 80 units, in Produce.
- One **Completed** order for **Walk-in Customer**, totaling 22.48 and timestamped with the current UTC time supplied by `TimeProvider`.
