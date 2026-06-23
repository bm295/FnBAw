# Market-Ready Product To-Do List

This checklist breaks the remaining work into small, clear tasks to prepare the FnB Management web application for a production launch and market release.

## 1. Product Scope and Documentation

- [x] Update `README.md` with a concise product overview for restaurant owners and food-service operators.
- [x] Add `docs/PRODUCT_REQUIREMENTS.md` describing the target users, core workflows, and launch success criteria.
- [x] Add `docs/USER_ROLES.md` defining permissions for owner, manager, cashier, kitchen staff, and inventory staff.
- [x] Add `docs/USER_STORIES.md` with small user stories for dashboard, menu, inventory, order, and reporting workflows.
- [x] Add `docs/RELEASE_CHECKLIST.md` with final pre-launch checks for security, data, performance, deployment, and support.

## 2. Application Configuration

- [x] Create `src/FnBManagement.Web/appsettings.Production.json` for production-safe logging and runtime settings.
- [x] Add environment variable documentation to `docs/ENVIRONMENT_VARIABLES.md`.
- [x] Add a `ConnectionStrings:DefaultConnection` placeholder to `src/FnBManagement.Web/appsettings.json`.
- [x] Create `src/FnBManagement.Web/Options/ApplicationOptions.cs` for product name, support email, currency, and timezone settings.
- [x] Register `ApplicationOptions` in `src/FnBManagement.Web/Program.cs`.

## 3. Data Persistence

- [x] Add `src/FnBManagement.Web/Data/FnBManagementDbContext.cs` for Entity Framework Core persistence.
- [x] Replace the temporary `src/FnBManagement.Web/Data/InMemoryStore.cs` data source with database-backed repositories.
- [x] Create `src/FnBManagement.Web/Data/Repositories/IMenuRepository.cs` for menu item data access.
- [x] Create `src/FnBManagement.Web/Data/Repositories/IInventoryRepository.cs` for inventory data access.
- [x] Create `src/FnBManagement.Web/Data/Repositories/IOrderRepository.cs` for order data access.
- [x] Add initial database migration under `src/FnBManagement.Web/Data/Migrations/`.
- [x] Add seed data in `src/FnBManagement.Web/Data/SeedData.cs` for demo restaurants, menu items, inventory, and orders.

## 4. Domain Models and Validation

- [x] Add validation attributes to `src/FnBManagement.Web/Models/MenuItem.cs` for name, category, price, and availability.
- [x] Add validation attributes to `src/FnBManagement.Web/Models/InventoryItem.cs` for name, unit, quantity, and reorder level.
- [x] Add validation attributes to `src/FnBManagement.Web/Models/Order.cs` for order total, order time, and ordered items.
- [x] Create `src/FnBManagement.Web/Models/OrderLineItem.cs` to support multiple items per order.
- [x] Add `src/FnBManagement.Web/Models/Restaurant.cs` for restaurant profile data.
- [x] Add `src/FnBManagement.Web/Models/Supplier.cs` for inventory supplier tracking.

## 5. Menu Management

- [x] Create `src/FnBManagement.Web/Controllers/MenuController.cs` with list, details, create, edit, and archive actions.
- [x] Create `src/FnBManagement.Web/Views/Menu/Index.cshtml` to list menu items with search and category filters.
- [x] Create `src/FnBManagement.Web/Views/Menu/Create.cshtml` for adding a new menu item.
- [ ] Create `src/FnBManagement.Web/Views/Menu/Edit.cshtml` for updating menu item details.
- [ ] Create `src/FnBManagement.Web/Views/Menu/Details.cshtml` to show menu item pricing, availability, and inventory links.
- [ ] Add server-side validation messages to all menu forms.
- [ ] Add confirmation flow before archiving a menu item.

## 6. Inventory Management

- [ ] Create `src/FnBManagement.Web/Controllers/InventoryController.cs` with list, create, edit, adjust stock, and reorder actions.
- [ ] Create `src/FnBManagement.Web/Views/Inventory/Index.cshtml` with low-stock filtering.
- [ ] Create `src/FnBManagement.Web/Views/Inventory/Create.cshtml` for adding inventory SKUs.
- [ ] Create `src/FnBManagement.Web/Views/Inventory/Edit.cshtml` for editing inventory details.
- [ ] Create `src/FnBManagement.Web/Views/Inventory/Adjust.cshtml` for stock count changes.
- [ ] Add inventory change history in `src/FnBManagement.Web/Models/InventoryTransaction.cs`.
- [ ] Add a reorder recommendation section to `src/FnBManagement.Web/Views/Inventory/Index.cshtml`.

## 7. Order Management

- [ ] Create `src/FnBManagement.Web/Controllers/OrdersController.cs` with list, details, create, and cancel actions.
- [ ] Create `src/FnBManagement.Web/Views/Orders/Index.cshtml` to show today’s orders and order status.
- [ ] Create `src/FnBManagement.Web/Views/Orders/Create.cshtml` for quick order entry.
- [ ] Create `src/FnBManagement.Web/Views/Orders/Details.cshtml` for itemized order details.
- [ ] Add `OrderStatus` values to `src/FnBManagement.Web/Models/Order.cs`.
- [ ] Deduct inventory automatically when an order is completed.
- [ ] Restore inventory automatically when an order is cancelled.

## 8. Dashboard and Reporting

- [ ] Update `src/FnBManagement.Web/Services/DashboardService.cs` to use persisted order, inventory, and menu repositories.
- [ ] Add gross margin KPI to `src/FnBManagement.Web/Models/DashboardViewModel.cs`.
- [ ] Add best-selling menu items KPI to `src/FnBManagement.Web/Models/DashboardViewModel.cs`.
- [ ] Update `src/FnBManagement.Web/Views/Home/Index.cshtml` with revenue, low-stock, best-seller, and margin sections.
- [ ] Create `src/FnBManagement.Web/Controllers/ReportsController.cs` for sales and inventory reports.
- [ ] Create `src/FnBManagement.Web/Views/Reports/Sales.cshtml` for date-range revenue reporting.
- [ ] Create `src/FnBManagement.Web/Views/Reports/Inventory.cshtml` for stock value and reorder reporting.
- [ ] Add CSV export support for sales reports.
- [ ] Add CSV export support for inventory reports.

## 9. Authentication and Authorization

- [ ] Add ASP.NET Core Identity to `src/FnBManagement.Web/FnBManagement.Web.csproj`.
- [ ] Create `src/FnBManagement.Web/Models/ApplicationUser.cs` for user profile data.
- [ ] Add login, logout, register, and forgot-password pages under `src/FnBManagement.Web/Views/Account/`.
- [ ] Require authentication for dashboard, menu, inventory, order, and report pages.
- [ ] Add role-based authorization policies in `src/FnBManagement.Web/Program.cs`.
- [ ] Restrict report access to owner and manager roles.
- [ ] Restrict inventory adjustments to manager and inventory staff roles.

## 10. User Experience and Branding

- [ ] Update `src/FnBManagement.Web/Views/Shared/_Layout.cshtml` with market-ready navigation for Dashboard, Menu, Inventory, Orders, and Reports.
- [ ] Add responsive mobile navigation to `src/FnBManagement.Web/wwwroot/css/site.css`.
- [ ] Add empty-state messages for lists with no menu items, inventory items, orders, or reports.
- [ ] Add success and error toast notifications to form submission flows.
- [ ] Add loading states for long-running report exports.
- [ ] Create `src/FnBManagement.Web/wwwroot/img/logo.svg` for product branding.
- [ ] Add a favicon at `src/FnBManagement.Web/wwwroot/favicon.ico`.

## 11. Accessibility and Localization

- [ ] Add descriptive page titles to every Razor view under `src/FnBManagement.Web/Views/`.
- [ ] Verify every form field has a visible label and validation message.
- [ ] Add keyboard-focus styles to `src/FnBManagement.Web/wwwroot/css/site.css`.
- [ ] Add ARIA labels to icon-only buttons and dashboard KPI sections.
- [ ] Add currency formatting helper based on configured market currency.
- [ ] Add timezone handling so daily reports match the restaurant’s local business day.

## 12. Security Hardening

- [ ] Enforce HTTPS redirection in `src/FnBManagement.Web/Program.cs` for production.
- [ ] Add secure cookie settings in `src/FnBManagement.Web/Program.cs`.
- [ ] Add anti-forgery tokens to all create, edit, delete, and adjustment forms.
- [ ] Validate uploaded files if image upload is added for menu items.
- [ ] Add audit logging for login, order cancellation, inventory adjustment, and menu price changes.
- [ ] Add rate limiting for login and report export endpoints.
- [ ] Review `infra/aws/ecs-task-definition.json` to ensure no secrets are committed.

## 13. Testing

- [ ] Create `tests/FnBManagement.Web.Tests/FnBManagement.Web.Tests.csproj` for unit and integration tests.
- [ ] Add unit tests for `src/FnBManagement.Web/Services/DashboardService.cs`.
- [ ] Add unit tests for menu validation rules.
- [ ] Add unit tests for inventory low-stock rules.
- [ ] Add unit tests for order total calculation.
- [ ] Add integration tests for menu create, edit, and archive flows.
- [ ] Add integration tests for inventory adjustment flows.
- [ ] Add integration tests for order creation and cancellation flows.
- [ ] Add accessibility smoke checks for key pages.
- [ ] Add a GitHub Actions workflow at `.github/workflows/ci.yml` to run build and tests on every pull request.

## 14. Observability and Support

- [ ] Add structured logging to `src/FnBManagement.Web/Program.cs`.
- [ ] Add request correlation IDs to application logs.
- [ ] Add health check endpoint at `/health` in `src/FnBManagement.Web/Program.cs`.
- [ ] Add readiness check for the database connection.
- [ ] Document support escalation steps in `docs/SUPPORT_RUNBOOK.md`.
- [ ] Document backup and restore steps in `docs/BACKUP_RESTORE.md`.

## 15. Deployment and Operations

- [ ] Update `infra/aws/Dockerfile` to use production build settings and a non-root runtime user.
- [ ] Update `infra/aws/ecs-task-definition.json` with production CPU, memory, health check, and logging settings.
- [ ] Update `infra/aws/DEPLOYMENT.md` with exact deployment commands and rollback instructions.
- [ ] Add infrastructure variables documentation to `infra/aws/README.md`.
- [ ] Add database migration steps to `infra/aws/DEPLOYMENT.md`.
- [ ] Add smoke-test steps to run immediately after deployment.

## 16. Launch Readiness

- [ ] Prepare a demo restaurant account with realistic sample data.
- [ ] Record screenshots for dashboard, menu, inventory, order, and reports pages.
- [ ] Create `docs/SALES_DEMO_SCRIPT.md` for product walkthroughs.
- [ ] Create `docs/FAQ.md` for common customer questions.
- [ ] Create `docs/PRICING_PACKAGING.md` with proposed pricing tiers and feature limits.
- [ ] Confirm privacy policy, terms of service, and data retention requirements before launch.
- [ ] Run final production smoke test and record results in `docs/RELEASE_CHECKLIST.md`.
