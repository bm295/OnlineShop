# Rewrite Decisions

## Task status

- [x] Write the chosen TypeScript stack.
- [x] Write the reasons for choosing Next.js.
- [x] Write the reasons for choosing Prisma.
- [x] Write the reasons for choosing PostgreSQL.
- [x] Write what will not be migrated from the old stack.

## Chosen TypeScript stack

- **Language:** TypeScript
- **Web framework:** Next.js
- **UI layer:** React components with Next.js routing and rendering
- **ORM:** Prisma
- **Database:** PostgreSQL
- **Validation:** TypeScript-first request and form validation at application boundaries
- **Styling:** Component-oriented CSS approach compatible with Next.js

## Why Next.js

- Provides a production-ready TypeScript and React foundation for replacing the current ASP.NET MVC UI.
- Supports file-based routing, layouts, server rendering, and client interactivity in one framework.
- Enables colocating page, route, and data-loading concerns while keeping the application approachable for a small shop inventory system.
- Has a broad ecosystem for authentication, forms, testing, deployment, and observability if the app grows beyond the current feature set.

## Why Prisma

- Provides a strongly typed database client generated from the schema, which fits the TypeScript rewrite goal.
- Makes the current model relationships straightforward to represent: categories have products, products belong to categories, and orders track status and totals.
- Offers migrations and schema management that are easier to review during a rewrite than ad hoc database changes.
- Keeps data access explicit and testable without carrying over Entity Framework-specific patterns.

## Why PostgreSQL

- Provides a durable production database option beyond the current local SQLite setup.
- Supports relational constraints, transactions, indexes, enums, and reporting queries suitable for inventory, categories, and orders.
- Works well with Prisma and common Next.js deployment targets.
- Leaves room for future operational needs such as backups, analytics, concurrency, and larger datasets.

## What will not be migrated from the old stack

- ASP.NET Core MVC controllers, Razor views, and Razor layout files will not be migrated directly; they will be replaced by Next.js routes and React UI.
- Entity Framework Core `DbContext` and SQLite provider configuration will not be migrated; Prisma and PostgreSQL will replace them.
- The local SQLite database file approach will not be carried forward as the primary runtime database.
- Server-side `ViewBag` patterns will not be migrated; typed props, server components, or API responses will replace them.
- The intentionally inefficient dashboard query pattern will not be preserved; the rewrite should use efficient aggregate and relation queries.
- C# model classes and enums will not be migrated as source files; they will be translated into TypeScript types and Prisma schema models.
