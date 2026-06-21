# User Roles and Permissions

Status: Done

## Permission principles

- Give each user the least access needed for their shift responsibilities.
- Reserve financial reporting, user management, and destructive actions for leadership roles.
- Keep operational workflows fast for service staff while protecting sensitive data.

## Roles

| Role | Primary purpose | Permissions |
| --- | --- | --- |
| Owner | Business oversight and account control | View all dashboards and reports; manage users and roles; manage restaurant settings; view and export sales data; approve launch and billing settings; perform all manager actions. |
| Manager | Daily operations leadership | View dashboard; manage menu items and availability; view and manage orders; adjust inventory; view operational reports; resolve low-stock alerts; supervise cashier, kitchen, and inventory workflows. |
| Cashier | Front-of-house order handling | Create orders; view today’s orders; update basic order status; view available menu items; cannot change prices, inventory quantities, users, or financial reports. |
| Kitchen Staff | Food preparation coordination | View active orders and item availability; mark preparation status when supported; view menu item details needed for service; cannot edit prices, create refunds, change inventory counts, or view financial reports. |
| Inventory Staff | Stock control and replenishment | View inventory list; create and edit inventory items; adjust stock counts; record receiving, waste, and count corrections; view low-stock alerts; cannot manage users, pricing, or sales reports unless also assigned manager access. |

## Workflow access matrix

| Workflow | Owner | Manager | Cashier | Kitchen Staff | Inventory Staff |
| --- | --- | --- | --- | --- | --- |
| Dashboard | Full | Full | Limited daily operations | Limited kitchen queue | Limited inventory alerts |
| Menu | Full | Create/edit/archive | View available items | View available items | View inventory-linked items |
| Inventory | Full | Full | No access | View availability only | Full stock operations |
| Orders | Full | Full | Create/update own shift orders | View/prepare orders | View inventory impact only |
| Reports | Full | Operational reports | No access | No access | Inventory reports only if granted |
| User management | Full | Invite or deactivate staff if delegated | No access | No access | No access |

## Completion checklist

- [x] Owner permissions defined.
- [x] Manager permissions defined.
- [x] Cashier permissions defined.
- [x] Kitchen staff permissions defined.
- [x] Inventory staff permissions defined.
