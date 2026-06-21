# Current App Inventory

## Task status

- [x] List every current C# model file.
- [x] Document `OnlineShop/Models/Product.cs` fields: `Id`, `Name`, `Sku`, `Price`, `StockQuantity`, `CategoryId`, and `Category`.
- [x] Document `OnlineShop/Models/Category.cs` fields.
- [x] Document `OnlineShop/Models/Order.cs` fields and enum values.
- [x] List every current controller file.
- [x] Document every route currently provided by `ProductsController`.
- [x] Document every route currently provided by `CategoriesController`.
- [x] Document every route currently provided by `OrdersController`.
- [x] Document every route currently provided by `DashboardController`.
- [x] List every Razor view file.
- [x] Describe the data currently seeded in `Program.cs`.

## Current C# model files

- `OnlineShop/Models/Category.cs`
- `OnlineShop/Models/Order.cs`
- `OnlineShop/Models/Product.cs`

## Model details

### `OnlineShop/Models/Product.cs`

| Field | Type | Notes |
| --- | --- | --- |
| `Id` | `int` | Product primary identifier. |
| `Name` | `string` | Product display name; initialized to `string.Empty`. |
| `Sku` | `string` | Product stock keeping unit; initialized to `string.Empty`. |
| `Price` | `decimal` | Product unit price. |
| `StockQuantity` | `int` | Current inventory quantity. |
| `CategoryId` | `int` | Foreign key to the product category. |
| `Category` | `Category?` | Optional navigation property for the associated category. |

### `OnlineShop/Models/Category.cs`

| Field | Type | Notes |
| --- | --- | --- |
| `Id` | `int` | Category primary identifier. |
| `Name` | `string` | Category display name; initialized to `string.Empty`. |
| `Description` | `string` | Category description; initialized to `string.Empty`. |
| `Products` | `ICollection<Product>` | Navigation collection of products in the category; initialized to an empty `List<Product>`. |

### `OnlineShop/Models/Order.cs`

| Field | Type | Notes |
| --- | --- | --- |
| `Id` | `int` | Order primary identifier. |
| `CustomerName` | `string` | Customer display name; initialized to `string.Empty`. |
| `TotalAmount` | `decimal` | Order total amount. |
| `CreatedAtUtc` | `DateTime` | UTC timestamp for when the order was created. |
| `Status` | `OrderStatus` | Current order status. |

#### `OrderStatus` enum values

| Value | Numeric value |
| --- | ---: |
| `Pending` | `0` |
| `Completed` | `1` |
| `Cancelled` | `2` |

## Current controller files

- `OnlineShop/Controllers/CategoriesController.cs`
- `OnlineShop/Controllers/DashboardController.cs`
- `OnlineShop/Controllers/OrdersController.cs`
- `OnlineShop/Controllers/ProductsController.cs`

## Current routes

The application uses the conventional route pattern `{controller=Dashboard}/{action=Index}/{id?}`. The routes below are the current conventional routes exposed by each controller action.

### `ProductsController`

| HTTP method | Route | Action | Notes |
| --- | --- | --- | --- |
| `GET` | `/Products` | `Index` | Lists products including categories, ordered by product name. |
| `GET` | `/Products/Index` | `Index` | Explicit conventional route for the same listing action. |
| `GET` | `/Products/Create` | `Create` | Shows the product creation form and populates category options. |
| `POST` | `/Products/Create` | `Create` | Validates and creates a product, then redirects to `Index`. |

### `CategoriesController`

| HTTP method | Route | Action | Notes |
| --- | --- | --- | --- |
| `GET` | `/Categories` | `Index` | Lists categories ordered by category name. |
| `GET` | `/Categories/Index` | `Index` | Explicit conventional route for the same listing action. |
| `GET` | `/Categories/Create` | `Create` | Shows the category creation form. |
| `POST` | `/Categories/Create` | `Create` | Validates and creates a category, then redirects to `Index`. |

### `OrdersController`

| HTTP method | Route | Action | Notes |
| --- | --- | --- | --- |
| `GET` | `/Orders` | `Index` | Lists orders ordered by newest `CreatedAtUtc` first. |
| `GET` | `/Orders/Index` | `Index` | Explicit conventional route for the same listing action. |
| `GET` | `/Orders/Create` | `Create` | Shows the order creation form with `CreatedAtUtc` initialized to the current UTC time. |
| `POST` | `/Orders/Create` | `Create` | Validates and creates an order, overwriting `CreatedAtUtc` with the current UTC time, then redirects to `Index`. |

### `DashboardController`

| HTTP method | Route | Action | Notes |
| --- | --- | --- | --- |
| `GET` | `/` | `Index` | Default route; shows dashboard metrics and recent orders. |
| `GET` | `/Dashboard` | `Index` | Controller default route for the dashboard. |
| `GET` | `/Dashboard/Index` | `Index` | Explicit conventional route for the dashboard action. |

## Current Razor view files

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

## Current seed data in `Program.cs`

On startup, the app calls `EnsureCreated()` for the configured SQLite database. If there are no categories, it seeds:

### Categories

- `Pantry` with description `Shelf-stable grocery items`.
- `Produce` with description `Fresh fruits and vegetables`.

### Products

- `Olive Oil` with SKU `PAN-001`, price `14.99`, stock quantity `34`, in `Pantry`.
- `Brown Rice` with SKU `PAN-002`, price `7.49`, stock quantity `50`, in `Pantry`.
- `Apples` with SKU `PRO-001`, price `3.25`, stock quantity `80`, in `Produce`.

### Orders

- One order for `Walk-in Customer`, created at the current UTC time, with total amount `22.48` and status `Completed`.
