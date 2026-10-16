# FnB Management (Angular / .NET 10)

FnB Management is a web application for restaurant owners and food-service operators who need one place to monitor daily sales, menu availability, inventory risk, and order activity. It helps operators spot low-stock ingredients, understand revenue for the current business day, keep menu items service-ready, and prepare for a more complete market launch with role-based workflows and reporting.

> Note: the project is configured for `net10.0` and uses the SDK default C# language version.

## Product overview

The application is designed for small and mid-sized restaurants, cafes, quick-service counters, catering kitchens, and multi-shift food-service teams. The initial product focuses on operational visibility: owners and managers can review dashboard KPIs, cashiers can register orders, kitchen teams can see what is sellable, and inventory staff can track reorder needs before shortages interrupt service.

## Features

- Dashboard with KPI cards for menu count, availability, inventory SKUs, low-stock items, daily orders, and daily revenue
- Menu item management planning for pricing, categories, and availability
- Inventory tracking with low-stock alerts and reorder thresholds
- Menu and inventory name searches require all search terms, ignoring case. Double quotes preserve phrases: `chicken "spicy sauce"` matches names containing both `chicken` and the contiguous phrase `spicy sauce`. Curly double quotes are also supported; an unclosed quote treats the remaining text as one phrase. Empty terms are ignored.
- Daily order registration and revenue summary
- Launch documentation for requirements, roles, user stories, and release readiness

## Target operators

- Independent restaurant owners who need quick insight into sales and stock health
- Food-service managers who coordinate front-of-house, kitchen, and inventory tasks
- Cashiers and counter staff who need fast order entry and clear order status
- Kitchen and inventory teams that need accurate availability and replenishment signals

## Project structure

- `client` - Angular client
- `src/FnBManagement.Web` - ASP.NET Core JSON API
- `infra/aws` - AWS deployment assets (Dockerfile, ECS task definition, and deployment notes)
- `docs` - product requirements, roles, user stories, and launch readiness documentation

### Backend architecture

The backend follows a lightweight Clean Architecture layout within one project:

```text
src/FnBManagement.Web/
  Controllers/                  HTTP endpoints
  Contracts/                    API request contracts
  Application/
    Abstractions/               Repository interfaces owned by use cases
    Dashboard/                  Dashboard service and result model
    Inventory/                  Inventory search use case
    Menu/                       Menu service and result model
    Search/                     Shared search term parser
  Domain/Entities/              Entities and business rules
  Infrastructure/Persistence/  EF Core context, repositories, migrations, seed data
  Configuration/               Host configuration checks
  Options/                      Host configuration models
  Program.cs                    Composition root and dependency injection
```

Application depends on Domain and repository interfaces, while Infrastructure implements those interfaces. Controllers use Application abstractions; they do not reference EF Core implementations. Domain and Application must not depend on Infrastructure or ASP.NET Core. Source dependency tests guard these conventions; separate assemblies do not yet enforce them.

This reorganization preserves existing routes, JSON shapes, validation attributes, and database tables. Existing CRUD endpoints still accept and return entities and call repository interfaces directly; dedicated CRUD use cases and separate API DTOs can be introduced when needed. Application result models retain their existing `ViewModel` names for continuity. EF migration metadata follows the relocated entity namespaces without introducing a new schema migration.

## Run locally

Requires .NET 10 SDK, Node.js 24, and a PostgreSQL connection string configured under `ConnectionStrings:DefaultConnection`. Run these in separate terminals:

```bash
dotnet run --project src/FnBManagement.Web/FnBManagement.Web.csproj --urls http://localhost:5080
```

```bash
cd client
npm ci
npm start
```

Open `http://localhost:4200`. The Angular dev server proxies `/api` to .NET. The AWS Dockerfile builds Angular and serves the resulting static files from .NET.

## AWS deployment options

- Docker image -> Amazon ECS Fargate
- Docker image -> AWS App Runner
- Containerized deployment behind ALB with CloudWatch logging

See `infra/aws/DEPLOYMENT.md` for complete deployment steps.

## Documentation status

- [x] Product overview for restaurant owners and food-service operators added.
- [x] Product requirements documented in `docs/PRODUCT_REQUIREMENTS.md`.
- [x] User roles documented in `docs/USER_ROLES.md`.
- [x] User stories documented in `docs/USER_STORIES.md`.
- [x] Release checklist documented in `docs/RELEASE_CHECKLIST.md`.
