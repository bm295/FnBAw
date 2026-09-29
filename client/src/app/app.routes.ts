import { Routes } from '@angular/router';
import { DashboardPage, InventoryPage, MenuPage } from './app';

export const routes: Routes = [
  { path: '', component: DashboardPage },
  { path: 'menu', component: MenuPage },
  { path: 'inventory', component: InventoryPage },
  { path: '**', redirectTo: '' }
];
