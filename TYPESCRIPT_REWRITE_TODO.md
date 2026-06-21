# TypeScript Rewrite TODO for OnlineShop

This checklist breaks the rewrite into small, ordered tasks for replacing the current ASP.NET Core MVC application with a TypeScript-based online shop. The target architecture is:

- **Frontend:** Next.js with TypeScript
- **Backend API:** Next.js route handlers at first, with an optional later split to NestJS
- **Database:** PostgreSQL for production and SQLite only for local experimentation if needed
- **ORM:** Prisma
- **Styling:** CSS modules or Tailwind CSS
- **Testing:** Vitest, React Testing Library, and Playwright

## Phase 1: Document the current application

- [ ] Create `docs/current-app-inventory.md`.
- [ ] In `docs/current-app-inventory.md`, list every current C# model file.
- [ ] In `docs/current-app-inventory.md`, document `OnlineShop/Models/Product.cs` fields: `Id`, `Name`, `Sku`, `Price`, `StockQuantity`, `CategoryId`, and `Category`.
- [ ] In `docs/current-app-inventory.md`, document `OnlineShop/Models/Category.cs` fields.
- [ ] In `docs/current-app-inventory.md`, document `OnlineShop/Models/Order.cs` fields and enum values.
- [ ] In `docs/current-app-inventory.md`, list every current controller file.
- [ ] In `docs/current-app-inventory.md`, document every route currently provided by `ProductsController`.
- [ ] In `docs/current-app-inventory.md`, document every route currently provided by `CategoriesController`.
- [ ] In `docs/current-app-inventory.md`, document every route currently provided by `OrdersController`.
- [ ] In `docs/current-app-inventory.md`, document every route currently provided by `DashboardController`.
- [ ] In `docs/current-app-inventory.md`, list every Razor view file.
- [ ] In `docs/current-app-inventory.md`, describe the data currently seeded in `Program.cs`.
- [ ] Create `docs/rewrite-decisions.md`.
- [ ] In `docs/rewrite-decisions.md`, write the chosen TypeScript stack.
- [ ] In `docs/rewrite-decisions.md`, write the reasons for choosing Next.js.
- [ ] In `docs/rewrite-decisions.md`, write the reasons for choosing Prisma.
- [ ] In `docs/rewrite-decisions.md`, write the reasons for choosing PostgreSQL.
- [ ] In `docs/rewrite-decisions.md`, write what will not be migrated from the old stack.

## Phase 2: Prepare repository structure

- [ ] Create `legacy-dotnet/`.
- [ ] Move `OnlineShop.sln` to `legacy-dotnet/OnlineShop.sln`.
- [ ] Move `Directory.Build.props` to `legacy-dotnet/Directory.Build.props`.
- [ ] Move `OnlineShop/` to `legacy-dotnet/OnlineShop/`.
- [ ] Create `apps/`.
- [ ] Create `apps/storefront/`.
- [ ] Create `packages/`.
- [ ] Create `packages/domain/`.
- [ ] Create `packages/config/`.
- [ ] Create `docs/` if it does not already exist.
- [ ] Create `.editorconfig` at the repository root.
- [ ] Create `.gitignore` at the repository root.
- [ ] Add `node_modules/` to `.gitignore`.
- [ ] Add `.next/` to `.gitignore`.
- [ ] Add `dist/` to `.gitignore`.
- [ ] Add `.env` to `.gitignore`.
- [ ] Add `.env.local` to `.gitignore`.
- [ ] Add `coverage/` to `.gitignore`.
- [ ] Add `playwright-report/` to `.gitignore`.
- [ ] Add `test-results/` to `.gitignore`.

## Phase 3: Initialize TypeScript workspace

- [ ] Create `package.json` at the repository root.
- [ ] In root `package.json`, set `private` to `true`.
- [ ] In root `package.json`, add a `workspaces` array containing `apps/*` and `packages/*`.
- [ ] In root `package.json`, add script `dev` that runs the storefront dev server.
- [ ] In root `package.json`, add script `build` that builds all workspaces.
- [ ] In root `package.json`, add script `lint` that lints all workspaces.
- [ ] In root `package.json`, add script `test` that runs unit tests.
- [ ] In root `package.json`, add script `test:e2e` that runs Playwright tests.
- [ ] Create `tsconfig.base.json` at the repository root.
- [ ] In `tsconfig.base.json`, enable `strict`.
- [ ] In `tsconfig.base.json`, enable `noUncheckedIndexedAccess`.
- [ ] In `tsconfig.base.json`, enable `exactOptionalPropertyTypes`.
- [ ] In `tsconfig.base.json`, configure path alias `@onlineshop/domain/*` for `packages/domain/src/*`.
- [ ] Create `eslint.config.mjs` at the repository root.
- [ ] Configure ESLint for TypeScript in `eslint.config.mjs`.
- [ ] Configure ESLint for React in `eslint.config.mjs`.
- [ ] Configure ESLint for Next.js in `eslint.config.mjs`.
- [ ] Create `prettier.config.mjs` at the repository root.
- [ ] Configure Prettier with consistent semicolon and quote rules.

## Phase 4: Create the Next.js application

- [ ] Initialize `apps/storefront/package.json`.
- [ ] Add dependency `next` to `apps/storefront/package.json`.
- [ ] Add dependency `react` to `apps/storefront/package.json`.
- [ ] Add dependency `react-dom` to `apps/storefront/package.json`.
- [ ] Add development dependency `typescript` to the workspace.
- [ ] Add development dependency `@types/node` to the workspace.
- [ ] Add development dependency `@types/react` to the workspace.
- [ ] Add development dependency `@types/react-dom` to the workspace.
- [ ] Create `apps/storefront/tsconfig.json`.
- [ ] Extend `../../tsconfig.base.json` from `apps/storefront/tsconfig.json`.
- [ ] Create `apps/storefront/next.config.ts`.
- [ ] Create `apps/storefront/src/`.
- [ ] Create `apps/storefront/src/app/`.
- [ ] Create `apps/storefront/src/app/layout.tsx`.
- [ ] Create `apps/storefront/src/app/page.tsx`.
- [ ] Create `apps/storefront/src/app/globals.css`.
- [ ] In `apps/storefront/src/app/layout.tsx`, define the root HTML layout.
- [ ] In `apps/storefront/src/app/page.tsx`, render a temporary dashboard page.
- [ ] In `apps/storefront/src/app/globals.css`, add base typography and layout styles.

## Phase 5: Create shared domain package

- [ ] Create `packages/domain/package.json`.
- [ ] Set package name to `@onlineshop/domain` in `packages/domain/package.json`.
- [ ] Create `packages/domain/tsconfig.json`.
- [ ] Create `packages/domain/src/`.
- [ ] Create `packages/domain/src/product.ts`.
- [ ] In `packages/domain/src/product.ts`, define `Product` type.
- [ ] In `packages/domain/src/product.ts`, define `CreateProductInput` type.
- [ ] In `packages/domain/src/product.ts`, define `UpdateProductInput` type.
- [ ] Create `packages/domain/src/category.ts`.
- [ ] In `packages/domain/src/category.ts`, define `Category` type.
- [ ] In `packages/domain/src/category.ts`, define `CreateCategoryInput` type.
- [ ] In `packages/domain/src/category.ts`, define `UpdateCategoryInput` type.
- [ ] Create `packages/domain/src/order.ts`.
- [ ] In `packages/domain/src/order.ts`, define `OrderStatus` enum or union type.
- [ ] In `packages/domain/src/order.ts`, define `Order` type.
- [ ] In `packages/domain/src/order.ts`, define `CreateOrderInput` type.
- [ ] Create `packages/domain/src/index.ts`.
- [ ] Export product types from `packages/domain/src/index.ts`.
- [ ] Export category types from `packages/domain/src/index.ts`.
- [ ] Export order types from `packages/domain/src/index.ts`.

## Phase 6: Add validation schemas

- [ ] Add dependency `zod` to the workspace.
- [ ] Create `packages/domain/src/product.schema.ts`.
- [ ] In `packages/domain/src/product.schema.ts`, create `productSchema`.
- [ ] In `packages/domain/src/product.schema.ts`, create `createProductSchema`.
- [ ] In `packages/domain/src/product.schema.ts`, require product name to be non-empty.
- [ ] In `packages/domain/src/product.schema.ts`, require SKU to be non-empty.
- [ ] In `packages/domain/src/product.schema.ts`, require price to be greater than or equal to zero.
- [ ] In `packages/domain/src/product.schema.ts`, require stock quantity to be an integer greater than or equal to zero.
- [ ] Create `packages/domain/src/category.schema.ts`.
- [ ] In `packages/domain/src/category.schema.ts`, create `categorySchema`.
- [ ] In `packages/domain/src/category.schema.ts`, create `createCategorySchema`.
- [ ] Create `packages/domain/src/order.schema.ts`.
- [ ] In `packages/domain/src/order.schema.ts`, create `orderSchema`.
- [ ] In `packages/domain/src/order.schema.ts`, create `createOrderSchema`.
- [ ] Export all schemas from `packages/domain/src/index.ts`.

## Phase 7: Configure database and Prisma

- [ ] Add dependency `@prisma/client` to the workspace.
- [ ] Add development dependency `prisma` to the workspace.
- [ ] Create `apps/storefront/prisma/`.
- [ ] Create `apps/storefront/prisma/schema.prisma`.
- [ ] In `schema.prisma`, configure the PostgreSQL datasource.
- [ ] In `schema.prisma`, configure the Prisma client generator.
- [ ] In `schema.prisma`, create `Category` model.
- [ ] In `schema.prisma`, create `Product` model.
- [ ] In `schema.prisma`, create `Order` model.
- [ ] In `schema.prisma`, create `OrderItem` model.
- [ ] Add a unique index on `Product.sku` in `schema.prisma`.
- [ ] Add a relation from `Product` to `Category` in `schema.prisma`.
- [ ] Add a relation from `OrderItem` to `Order` in `schema.prisma`.
- [ ] Add a relation from `OrderItem` to `Product` in `schema.prisma`.
- [ ] Create `apps/storefront/src/lib/`.
- [ ] Create `apps/storefront/src/lib/prisma.ts`.
- [ ] In `apps/storefront/src/lib/prisma.ts`, create a singleton Prisma client.
- [ ] Create `apps/storefront/.env.example`.
- [ ] Add `DATABASE_URL` to `apps/storefront/.env.example`.
- [ ] Add script `db:generate` to `apps/storefront/package.json`.
- [ ] Add script `db:migrate` to `apps/storefront/package.json`.
- [ ] Add script `db:seed` to `apps/storefront/package.json`.

## Phase 8: Recreate seed data

- [ ] Create `apps/storefront/prisma/seed.ts`.
- [ ] In `seed.ts`, create category `Pantry`.
- [ ] In `seed.ts`, create category `Produce`.
- [ ] In `seed.ts`, create product `Olive Oil` with SKU `PAN-001`.
- [ ] In `seed.ts`, create product `Brown Rice` with SKU `PAN-002`.
- [ ] In `seed.ts`, create product `Apples` with SKU `PRO-001`.
- [ ] In `seed.ts`, create sample order for `Walk-in Customer`.
- [ ] Make `seed.ts` idempotent by using upsert operations.
- [ ] Add Prisma seed configuration to `apps/storefront/package.json`.
- [ ] Run Prisma generate.
- [ ] Run the initial migration.
- [ ] Run the seed script.
- [ ] Confirm seeded products appear in the database.

## Phase 9: Build product data access layer

- [ ] Create `apps/storefront/src/features/`.
- [ ] Create `apps/storefront/src/features/products/`.
- [ ] Create `apps/storefront/src/features/products/product.repository.ts`.
- [ ] In `product.repository.ts`, create function `listProducts`.
- [ ] In `product.repository.ts`, make `listProducts` include each product category.
- [ ] In `product.repository.ts`, make `listProducts` sort by product name.
- [ ] In `product.repository.ts`, create function `getProductById`.
- [ ] In `product.repository.ts`, create function `createProduct`.
- [ ] In `product.repository.ts`, validate `createProduct` input with `createProductSchema`.
- [ ] In `product.repository.ts`, create function `updateProduct`.
- [ ] In `product.repository.ts`, create function `deleteProduct`.
- [ ] Create `apps/storefront/src/features/products/product.mapper.ts`.
- [ ] In `product.mapper.ts`, map Prisma product rows to domain `Product` objects.

## Phase 10: Build category data access layer

- [ ] Create `apps/storefront/src/features/categories/`.
- [ ] Create `apps/storefront/src/features/categories/category.repository.ts`.
- [ ] In `category.repository.ts`, create function `listCategories`.
- [ ] In `category.repository.ts`, sort categories by name.
- [ ] In `category.repository.ts`, create function `getCategoryById`.
- [ ] In `category.repository.ts`, create function `createCategory`.
- [ ] In `category.repository.ts`, validate `createCategory` input with `createCategorySchema`.
- [ ] In `category.repository.ts`, create function `updateCategory`.
- [ ] In `category.repository.ts`, create function `deleteCategory`.
- [ ] Create `apps/storefront/src/features/categories/category.mapper.ts`.
- [ ] In `category.mapper.ts`, map Prisma category rows to domain `Category` objects.

## Phase 11: Build order data access layer

- [ ] Create `apps/storefront/src/features/orders/`.
- [ ] Create `apps/storefront/src/features/orders/order.repository.ts`.
- [ ] In `order.repository.ts`, create function `listOrders`.
- [ ] In `order.repository.ts`, sort orders by creation date descending.
- [ ] In `order.repository.ts`, create function `getOrderById`.
- [ ] In `order.repository.ts`, create function `createOrder`.
- [ ] In `order.repository.ts`, validate `createOrder` input with `createOrderSchema`.
- [ ] In `order.repository.ts`, calculate order totals from order items.
- [ ] In `order.repository.ts`, wrap order creation in a database transaction.
- [ ] In `order.repository.ts`, reduce product stock quantities during order creation.
- [ ] Create `apps/storefront/src/features/orders/order.mapper.ts`.
- [ ] In `order.mapper.ts`, map Prisma order rows to domain `Order` objects.

## Phase 12: Recreate product pages

- [ ] Create `apps/storefront/src/app/products/`.
- [ ] Create `apps/storefront/src/app/products/page.tsx`.
- [ ] In `products/page.tsx`, call `listProducts`.
- [ ] In `products/page.tsx`, render a page title `Products`.
- [ ] In `products/page.tsx`, render an `Add Product` link.
- [ ] In `products/page.tsx`, render a table of products.
- [ ] In `products/page.tsx`, render product name.
- [ ] In `products/page.tsx`, render product SKU.
- [ ] In `products/page.tsx`, render product category name.
- [ ] In `products/page.tsx`, render product price.
- [ ] In `products/page.tsx`, render product stock quantity.
- [ ] Create `apps/storefront/src/app/products/new/`.
- [ ] Create `apps/storefront/src/app/products/new/page.tsx`.
- [ ] Create `apps/storefront/src/features/products/ProductForm.tsx`.
- [ ] In `ProductForm.tsx`, render input for product name.
- [ ] In `ProductForm.tsx`, render input for SKU.
- [ ] In `ProductForm.tsx`, render input for price.
- [ ] In `ProductForm.tsx`, render input for stock quantity.
- [ ] In `ProductForm.tsx`, render select input for category.
- [ ] In `ProductForm.tsx`, render submit button.
- [ ] Create `apps/storefront/src/app/products/actions.ts`.
- [ ] In `products/actions.ts`, create server action `createProductAction`.
- [ ] In `createProductAction`, validate form data.
- [ ] In `createProductAction`, call `createProduct`.
- [ ] In `createProductAction`, redirect to `/products` on success.

## Phase 13: Recreate category pages

- [ ] Create `apps/storefront/src/app/categories/`.
- [ ] Create `apps/storefront/src/app/categories/page.tsx`.
- [ ] In `categories/page.tsx`, call `listCategories`.
- [ ] In `categories/page.tsx`, render a page title `Categories`.
- [ ] In `categories/page.tsx`, render an `Add Category` link.
- [ ] In `categories/page.tsx`, render a table of categories.
- [ ] Create `apps/storefront/src/app/categories/new/`.
- [ ] Create `apps/storefront/src/app/categories/new/page.tsx`.
- [ ] Create `apps/storefront/src/features/categories/CategoryForm.tsx`.
- [ ] In `CategoryForm.tsx`, render input for category name.
- [ ] In `CategoryForm.tsx`, render textarea for category description.
- [ ] In `CategoryForm.tsx`, render submit button.
- [ ] Create `apps/storefront/src/app/categories/actions.ts`.
- [ ] In `categories/actions.ts`, create server action `createCategoryAction`.
- [ ] In `createCategoryAction`, validate form data.
- [ ] In `createCategoryAction`, call `createCategory`.
- [ ] In `createCategoryAction`, redirect to `/categories` on success.

## Phase 14: Recreate order pages

- [ ] Create `apps/storefront/src/app/orders/`.
- [ ] Create `apps/storefront/src/app/orders/page.tsx`.
- [ ] In `orders/page.tsx`, call `listOrders`.
- [ ] In `orders/page.tsx`, render a page title `Orders`.
- [ ] In `orders/page.tsx`, render an `Add Order` link.
- [ ] In `orders/page.tsx`, render customer name.
- [ ] In `orders/page.tsx`, render order created date.
- [ ] In `orders/page.tsx`, render order total amount.
- [ ] In `orders/page.tsx`, render order status.
- [ ] Create `apps/storefront/src/app/orders/new/`.
- [ ] Create `apps/storefront/src/app/orders/new/page.tsx`.
- [ ] Create `apps/storefront/src/features/orders/OrderForm.tsx`.
- [ ] In `OrderForm.tsx`, render input for customer name.
- [ ] In `OrderForm.tsx`, render product selector.
- [ ] In `OrderForm.tsx`, render quantity input for each order item.
- [ ] In `OrderForm.tsx`, render submit button.
- [ ] Create `apps/storefront/src/app/orders/actions.ts`.
- [ ] In `orders/actions.ts`, create server action `createOrderAction`.
- [ ] In `createOrderAction`, validate form data.
- [ ] In `createOrderAction`, call `createOrder`.
- [ ] In `createOrderAction`, redirect to `/orders` on success.

## Phase 15: Recreate dashboard

- [ ] Create `apps/storefront/src/app/dashboard/`.
- [ ] Create `apps/storefront/src/app/dashboard/page.tsx`.
- [ ] Create `apps/storefront/src/features/dashboard/`.
- [ ] Create `apps/storefront/src/features/dashboard/dashboard.repository.ts`.
- [ ] In `dashboard.repository.ts`, create function `getDashboardStats`.
- [ ] In `getDashboardStats`, calculate total product count.
- [ ] In `getDashboardStats`, calculate total category count.
- [ ] In `getDashboardStats`, calculate total order count.
- [ ] In `getDashboardStats`, calculate total revenue.
- [ ] In `dashboard/page.tsx`, render product count card.
- [ ] In `dashboard/page.tsx`, render category count card.
- [ ] In `dashboard/page.tsx`, render order count card.
- [ ] In `dashboard/page.tsx`, render revenue card.
- [ ] Redirect `/` to `/dashboard` or render the dashboard on `/`.

## Phase 16: Build shared UI components

- [ ] Create `apps/storefront/src/components/`.
- [ ] Create `apps/storefront/src/components/AppShell.tsx`.
- [ ] In `AppShell.tsx`, render the main page layout.
- [ ] Create `apps/storefront/src/components/Navigation.tsx`.
- [ ] In `Navigation.tsx`, add link to `/dashboard`.
- [ ] In `Navigation.tsx`, add link to `/products`.
- [ ] In `Navigation.tsx`, add link to `/categories`.
- [ ] In `Navigation.tsx`, add link to `/orders`.
- [ ] Create `apps/storefront/src/components/Button.tsx`.
- [ ] Create `apps/storefront/src/components/Card.tsx`.
- [ ] Create `apps/storefront/src/components/Table.tsx`.
- [ ] Create `apps/storefront/src/components/FormField.tsx`.
- [ ] Create `apps/storefront/src/components/ErrorMessage.tsx`.
- [ ] Replace duplicated page markup with shared components.

## Phase 17: Add API routes if needed

- [ ] Create `apps/storefront/src/app/api/`.
- [ ] Create `apps/storefront/src/app/api/products/route.ts`.
- [ ] In `api/products/route.ts`, implement `GET` for product listing.
- [ ] In `api/products/route.ts`, implement `POST` for product creation.
- [ ] Create `apps/storefront/src/app/api/products/[id]/route.ts`.
- [ ] In `api/products/[id]/route.ts`, implement `GET` for one product.
- [ ] In `api/products/[id]/route.ts`, implement `PUT` for product update.
- [ ] In `api/products/[id]/route.ts`, implement `DELETE` for product deletion.
- [ ] Create `apps/storefront/src/app/api/categories/route.ts`.
- [ ] Implement category API routes.
- [ ] Create `apps/storefront/src/app/api/orders/route.ts`.
- [ ] Implement order API routes.
- [ ] Validate all API request bodies with Zod schemas.
- [ ] Return consistent JSON error responses.

## Phase 18: Add authentication and authorization

- [ ] Choose an authentication solution.
- [ ] Document the choice in `docs/rewrite-decisions.md`.
- [ ] Add dependency `next-auth` or chosen auth package.
- [ ] Create `apps/storefront/src/auth.ts` if using NextAuth.
- [ ] Create login page at `apps/storefront/src/app/login/page.tsx`.
- [ ] Create logout action.
- [ ] Create protected route helper.
- [ ] Require authentication for dashboard page.
- [ ] Require authentication for product create page.
- [ ] Require authentication for category create page.
- [ ] Require authentication for order create page.
- [ ] Add user model to `schema.prisma` if needed.
- [ ] Create migration for auth tables.

## Phase 19: Add cart and checkout foundation

- [ ] Create `apps/storefront/src/features/cart/`.
- [ ] Create `apps/storefront/src/features/cart/cart.types.ts`.
- [ ] Define `CartItem` type.
- [ ] Define `Cart` type.
- [ ] Create `apps/storefront/src/features/cart/cart.storage.ts`.
- [ ] Implement cart persistence with cookies or database sessions.
- [ ] Create `apps/storefront/src/app/cart/page.tsx`.
- [ ] Render cart items on `/cart`.
- [ ] Render cart subtotal on `/cart`.
- [ ] Create `apps/storefront/src/app/checkout/page.tsx`.
- [ ] Render checkout form on `/checkout`.
- [ ] Convert cart items to an order during checkout.
- [ ] Clear the cart after successful order creation.

## Phase 20: Add automated tests

- [ ] Add dependency `vitest`.
- [ ] Add dependency `@testing-library/react`.
- [ ] Add dependency `@testing-library/jest-dom`.
- [ ] Add dependency `jsdom`.
- [ ] Create `vitest.config.ts` at the repository root.
- [ ] Create `apps/storefront/src/test/`.
- [ ] Create `apps/storefront/src/test/setup.ts`.
- [ ] Add test script to root `package.json`.
- [ ] Create `packages/domain/src/product.schema.test.ts`.
- [ ] Test valid product input.
- [ ] Test invalid empty product name.
- [ ] Test invalid negative product price.
- [ ] Test invalid negative stock quantity.
- [ ] Create `packages/domain/src/category.schema.test.ts`.
- [ ] Create `packages/domain/src/order.schema.test.ts`.
- [ ] Create `apps/storefront/src/features/products/product.repository.test.ts`.
- [ ] Test product listing order.
- [ ] Test product creation validation.
- [ ] Create `apps/storefront/src/features/orders/order.repository.test.ts`.
- [ ] Test order total calculation.
- [ ] Test inventory decrement after order creation.

## Phase 21: Add end-to-end tests

- [ ] Add dependency `@playwright/test`.
- [ ] Create `playwright.config.ts` at the repository root.
- [ ] Create `e2e/`.
- [ ] Create `e2e/products.spec.ts`.
- [ ] Test navigating to `/products`.
- [ ] Test creating a product.
- [ ] Test the created product appears in the product table.
- [ ] Create `e2e/categories.spec.ts`.
- [ ] Test creating a category.
- [ ] Create `e2e/orders.spec.ts`.
- [ ] Test creating an order.
- [ ] Add script `test:e2e` to root `package.json`.
- [ ] Document how to run Playwright in `docs/testing.md`.

## Phase 22: Add error handling and empty states

- [ ] Create `apps/storefront/src/app/error.tsx`.
- [ ] Create `apps/storefront/src/app/not-found.tsx`.
- [ ] Add empty state to product list.
- [ ] Add empty state to category list.
- [ ] Add empty state to order list.
- [ ] Add form-level validation error display to `ProductForm.tsx`.
- [ ] Add field-level validation error display to `ProductForm.tsx`.
- [ ] Add form-level validation error display to `CategoryForm.tsx`.
- [ ] Add field-level validation error display to `CategoryForm.tsx`.
- [ ] Add form-level validation error display to `OrderForm.tsx`.
- [ ] Add field-level validation error display to `OrderForm.tsx`.
- [ ] Add duplicate SKU error handling.
- [ ] Add insufficient inventory error handling.

## Phase 23: Add formatting and quality gates

- [ ] Add script `format` to root `package.json`.
- [ ] Add script `format:check` to root `package.json`.
- [ ] Add script `typecheck` to root `package.json`.
- [ ] Add script `ci` to root `package.json`.
- [ ] Make `ci` run lint.
- [ ] Make `ci` run typecheck.
- [ ] Make `ci` run unit tests.
- [ ] Make `ci` run build.
- [ ] Create `.github/`.
- [ ] Create `.github/workflows/`.
- [ ] Create `.github/workflows/ci.yml`.
- [ ] Configure CI to install Node.js.
- [ ] Configure CI to install dependencies.
- [ ] Configure CI to run `npm run ci`.

## Phase 24: Prepare data migration plan

- [ ] Create `docs/data-migration.md`.
- [ ] Document how to export data from SQLite.
- [ ] Document how to transform old product rows to new product rows.
- [ ] Document how to transform old category rows to new category rows.
- [ ] Document how to transform old order rows to new order rows.
- [ ] Create `scripts/`.
- [ ] Create `scripts/export-legacy-sqlite.ts`.
- [ ] In `export-legacy-sqlite.ts`, read the legacy SQLite database.
- [ ] In `export-legacy-sqlite.ts`, export categories to JSON.
- [ ] In `export-legacy-sqlite.ts`, export products to JSON.
- [ ] In `export-legacy-sqlite.ts`, export orders to JSON.
- [ ] Create `scripts/import-to-prisma.ts`.
- [ ] In `import-to-prisma.ts`, read exported JSON files.
- [ ] In `import-to-prisma.ts`, upsert categories.
- [ ] In `import-to-prisma.ts`, upsert products.
- [ ] In `import-to-prisma.ts`, upsert orders.
- [ ] Add dry-run mode to `import-to-prisma.ts`.

## Phase 25: Documentation updates

- [ ] Replace root `README.md` or create one if missing.
- [ ] In `README.md`, describe the new TypeScript stack.
- [ ] In `README.md`, document prerequisites.
- [ ] In `README.md`, document environment variables.
- [ ] In `README.md`, document how to install dependencies.
- [ ] In `README.md`, document how to run the dev server.
- [ ] In `README.md`, document how to run migrations.
- [ ] In `README.md`, document how to seed the database.
- [ ] In `README.md`, document how to run tests.
- [ ] In `README.md`, document how to run production build.
- [ ] Create `docs/deployment.md`.
- [ ] In `docs/deployment.md`, document deployment target.
- [ ] In `docs/deployment.md`, document database provisioning.
- [ ] In `docs/deployment.md`, document secrets configuration.
- [ ] In `docs/deployment.md`, document migration execution during deployment.

## Phase 26: Final cutover checklist

- [ ] Confirm every old dashboard feature exists in TypeScript.
- [ ] Confirm every old product feature exists in TypeScript.
- [ ] Confirm every old category feature exists in TypeScript.
- [ ] Confirm every old order feature exists in TypeScript.
- [ ] Confirm seed data works in the new app.
- [ ] Confirm product SKU uniqueness works in the new app.
- [ ] Confirm decimal price storage works in the new app.
- [ ] Confirm order totals are calculated correctly.
- [ ] Confirm inventory decreases after order creation.
- [ ] Run `npm run lint`.
- [ ] Run `npm run typecheck`.
- [ ] Run `npm run test`.
- [ ] Run `npm run test:e2e`.
- [ ] Run `npm run build`.
- [ ] Take screenshots of dashboard page.
- [ ] Take screenshots of products page.
- [ ] Take screenshots of categories page.
- [ ] Take screenshots of orders page.
- [ ] Review accessibility of all forms.
- [ ] Review responsive layout on mobile width.
- [ ] Review responsive layout on desktop width.
- [ ] Archive or delete `legacy-dotnet/` only after the TypeScript version is verified.

## Phase 27: Optional future improvements

- [ ] Add product image upload.
- [ ] Add product search.
- [ ] Add product filters by category.
- [ ] Add low-stock warnings.
- [ ] Add customer accounts.
- [ ] Add payment provider integration.
- [ ] Add order confirmation emails.
- [ ] Add admin roles.
- [ ] Add audit logging.
- [ ] Add inventory history.
- [ ] Add dashboard charts.
- [ ] Add API rate limiting.
- [ ] Add structured logging.
- [ ] Add error tracking.
- [ ] Add caching for product lists.
- [ ] Add Dockerfile.
- [ ] Add docker-compose file for local PostgreSQL.
