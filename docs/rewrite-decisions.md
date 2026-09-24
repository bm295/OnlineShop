# TypeScript Rewrite Decisions

## Chosen Stack

- **Application framework:** Next.js with TypeScript, initially as one deployable application.
- **Frontend:** React through Next.js, using the App Router and server components by default. Client components will be limited to interactions that require browser state or APIs.
- **Backend:** Next.js route handlers for the initial HTTP API. A separate NestJS service may be considered later only if operational or domain complexity justifies it.
- **Database:** PostgreSQL in production.
- **ORM:** Prisma for schema definition, migrations, and typed database access.
- **Styling:** CSS modules for component-scoped styles, with Tailwind CSS remaining an option if a utility-first design system is adopted during implementation.
- **Testing:** Vitest for unit tests, React Testing Library for component behavior, and Playwright for end-to-end browser coverage.

## Why Next.js

Next.js keeps the storefront UI and its initial server-side API in one TypeScript codebase. It provides file-based routing, server rendering, server components, route handlers, and production build tooling without requiring separate frontend and backend projects at the start. This reduces deployment and type-sharing overhead while the replacement application is still small. Its conventions also provide a clear path for incremental feature migration.

## Why Prisma

Prisma provides a declarative schema, repeatable migrations, and a generated type-safe client that fits the TypeScript application. Relations such as products belonging to categories can be represented explicitly, while generated types reduce duplication between database access code and application logic. Prisma also supports PostgreSQL directly and makes local database setup and seed scripts straightforward.

## Why PostgreSQL

PostgreSQL is a production-grade relational database suited to the application's structured products, categories, and orders. It supports transactions, constraints, indexes, precise numeric types for money values, and a mature operational ecosystem. Using it as the production target avoids depending on SQLite behavior that can differ in concurrency, typing, and migration scenarios. SQLite may still be used for isolated local experiments, but it is not the production contract.

## What Will Not Be Migrated

The rewrite will preserve user-visible business capabilities and data concepts, not the old framework implementation. The following will not be carried into the new application:

- ASP.NET Core MVC controllers, conventional controller/action routing, Razor views, and C# view models.
- Entity Framework Core configuration, `ShopDbContext`, and the `EnsureCreated()` startup initialization path.
- The SQLite production configuration and the existing `shop.db` file as an application dependency. Any real data that must survive will use an explicit one-time migration into PostgreSQL rather than reusing the file at runtime.
- .NET-specific dependency injection, configuration bootstrapping, project files, build artifacts, and hosting middleware.
- The dashboard's intentionally inefficient per-order database queries and mutation of `CustomerName` to display computed counts. Equivalent dashboard information will be computed explicitly and efficiently.
- Accidental framework behavior that is not part of the intended product, including broad HTTP-method matching on unannotated actions.

The legacy application will remain available as a behavioral reference until replacement features and data migration are verified, but its implementation will not be embedded in or called by the new runtime.
