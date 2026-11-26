# ADR: MediatR request/handler boundary for the legacy .NET application

## Status
Accepted for the **legacy .NET MVC application only**.

## Context
The current repository is migrating toward a TypeScript / Next.js application. Its
ASP.NET MVC implementation under `legacy-dotnet/` is intentionally retained as
a behavioral reference. Legacy controllers directly used `ShopDbContext`, mixing
HTTP concerns with persistence operations.

## Decision
Use MediatR 12 in `legacy-dotnet/OnlineShop` to route the existing category,
product, order and dashboard actions through explicit queries and commands.
Controllers are responsible for MVC model validation and view/redirect responses;
request handlers own database operations. Register handlers by assembly scanning.
Use `ISender` to make controller intent explicit and propagate cancellation tokens.

MediatR runs in-process; this is **not** distributed messaging, event sourcing, or
a requirement to introduce a separate service. The request boundary permits
application-level testing and future cross-cutting behaviors when justified.

## Consequences
- Request/handler classes add indirection and code, which is acceptable for the
  small legacy application as a CQRS/Mediator demonstration.
- Entity models continue to be passed through the request boundary to preserve
  existing MVC behavior. This is not a full domain/application isolation effort.
- The dashboard's existing presentation text is preserved; its inefficient query
  pattern remains a separate performance refactoring concern.
- Existing views, URLs, redirects, and validation are preserved.
- This decision **does not apply** to `apps/storefront` or `packages/domain`.
  The TypeScript rewrite does not take a dependency on .NET or MediatR.
- Future improvements should assess transactional boundaries, handler validation,
  read-only projections, and query performance as independent decisions.
