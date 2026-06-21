# Product Requirements

Status: Done

## Product goal

FnB Management gives restaurant owners and food-service operators a simple operational control center for menu availability, inventory readiness, daily orders, and revenue visibility.

## Target users

- **Owners:** Need sales visibility, operational health, role controls, and confidence before opening each day.
- **Managers:** Need to coordinate menu updates, inventory exceptions, shift activity, and end-of-day reporting.
- **Cashiers:** Need quick order entry and order status visibility during service.
- **Kitchen staff:** Need accurate item availability and order demand signals.
- **Inventory staff:** Need low-stock alerts, reorder priorities, and stock adjustment workflows.

## Core workflows

### Dashboard monitoring

1. User opens the dashboard.
2. System shows daily orders, revenue, menu availability, inventory SKU count, and low-stock alerts.
3. User reviews recent orders and exceptions before taking action.

### Menu management

1. Manager reviews active menu items by category.
2. Manager adds or updates item name, price, category, and availability.
3. System reflects unavailable items in operational views.

### Inventory management

1. Inventory staff reviews current stock levels and reorder thresholds.
2. Staff records stock adjustments after receiving, waste, or physical counts.
3. System highlights items at or below reorder level.

### Order management

1. Cashier creates a new order during service.
2. System stores order total and timestamp.
3. Dashboard revenue and recent-order summaries update for daily visibility.

### Reporting

1. Owner or manager selects a reporting period.
2. System summarizes sales, order volume, and inventory exceptions.
3. User exports or reviews data for operational decisions.

## Launch success criteria

- Owners can understand daily revenue and low-stock risk within one minute of opening the dashboard.
- Managers can maintain menu availability without developer support.
- Cashiers can create orders consistently during a live shift.
- Inventory staff can identify reorder needs before stockouts affect sales.
- Role-based access rules are documented and implemented before production launch.
- Production deployment has security, data, performance, support, and rollback checks completed.

## Completion checklist

- [x] Target users documented.
- [x] Core workflows documented.
- [x] Launch success criteria documented.
