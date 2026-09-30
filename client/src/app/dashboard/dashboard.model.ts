import { InventoryItem } from '../inventory/inventory.model';

export interface Order { id: number; orderedAtUtc: string; total: number }
export interface Dashboard { menuItemsCount: number; availableMenuItemsCount: number; inventoryItemsCount: number; lowStockItemsCount: number; ordersTodayCount: number; revenueToday: number; lowStockItems: InventoryItem[]; recentOrders: Order[] }
