# Rewrite Decisions

## Chosen TypeScript stack

- Next.js with TypeScript for the full-stack web application.
- Prisma as the data access layer and migration tool.
- PostgreSQL as the relational database.
- React components and Next.js routing for the user interface.

## Why Next.js

- It provides a single TypeScript framework for the UI and server-side application code.
- File-based routing gives the rewrite a clear structure that maps well from the existing MVC controller and view organization.
- Server-side rendering and server components can support dashboard and inventory pages that need database-backed data at request time.
- API routes or server actions provide a straightforward replacement for current form-post controller actions.
- The ecosystem is mature, widely documented, and well supported for deployment.

## Why Prisma

- Prisma gives strongly typed database access from TypeScript, reducing mismatch between application models and database schema.
- Its schema file and generated client make the product, category, and order models explicit and easy to review.
- Prisma migrations provide a repeatable way to evolve the database during and after the rewrite.
- Relations such as `Product` to `Category` can be modeled directly and queried ergonomically.

## Why PostgreSQL

- PostgreSQL is a production-ready relational database with strong support for constraints, transactions, indexes, and relational queries.
- It fits the existing relational shape of categories, products, and orders.
- It works well with Prisma and common Next.js deployment platforms.
- It provides a better long-term production target than the current local SQLite setup.

## What will not be migrated from the old stack

- ASP.NET Core MVC controllers, Razor views, and C# model classes will not be carried forward directly.
- Entity Framework Core `DbContext`, EF-specific configuration, and SQLite setup will not be migrated.
- The current `Program.cs` hosting and middleware setup will be replaced by Next.js application configuration.
- The intentionally inefficient dashboard query pattern will not be preserved; the rewrite should use efficient aggregate and relation queries.
- Server-rendered Razor layout and view conventions will be replaced by React and Next.js layout conventions.
