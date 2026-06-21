# FnB Management Web Application (.NET 14 / C# 10)

FnB Management is a web application for restaurant owners and food-service operators who need one place to monitor daily sales, menu availability, inventory risk, and order activity. It helps operators spot low-stock ingredients, understand revenue for the current business day, keep menu items service-ready, and prepare for a more complete market launch with role-based workflows and reporting.

> Note: the project is configured for `net14.0` per requirement and locks language version to C# 10.

## Product overview

The application is designed for small and mid-sized restaurants, cafes, quick-service counters, catering kitchens, and multi-shift food-service teams. The initial product focuses on operational visibility: owners and managers can review dashboard KPIs, cashiers can register orders, kitchen teams can see what is sellable, and inventory staff can track reorder needs before shortages interrupt service.

## Features

- Dashboard with KPI cards for menu count, availability, inventory SKUs, low-stock items, daily orders, and daily revenue
- Menu item management planning for pricing, categories, and availability
- Inventory tracking with low-stock alerts and reorder thresholds
- Daily order registration and revenue summary
- Launch documentation for requirements, roles, user stories, and release readiness

## Target operators

- Independent restaurant owners who need quick insight into sales and stock health
- Food-service managers who coordinate front-of-house, kitchen, and inventory tasks
- Cashiers and counter staff who need fast order entry and clear order status
- Kitchen and inventory teams that need accurate availability and replenishment signals

## Project structure

- `src/FnBManagement.Web` - ASP.NET Core MVC application
- `infra/aws` - AWS deployment assets (Dockerfile, ECS task definition, and deployment notes)
- `docs` - product requirements, roles, user stories, and launch readiness documentation

## Run locally (when .NET 14 SDK is available)

```bash
dotnet run --project src/FnBManagement.Web/FnBManagement.Web.csproj
```

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
