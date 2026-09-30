import { Routes } from '@angular/router';
import { DashboardPage } from './dashboard/dashboard-page';
import { InventoryPage } from './inventory/inventory-page';
import { MenuPage } from './menu/menu-page';

export const routes: Routes = [
  { path: '', component: DashboardPage },
  { path: 'menu', component: MenuPage },
  { path: 'inventory', component: InventoryPage },
  { path: '**', redirectTo: '' }
];
