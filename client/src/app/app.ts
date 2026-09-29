import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterOutlet } from '@angular/router';

export interface MenuItem { id: number; name: string; category: string; price: number; isAvailable: boolean }
export interface InventoryItem { id: number; name: string; unit: string; quantityInStock: number; reorderLevel: number; isLowStock: boolean }
interface Order { id: number; orderedAtUtc: string; total: number }
interface Dashboard { menuItemsCount: number; availableMenuItemsCount: number; inventoryItemsCount: number; lowStockItemsCount: number; ordersTodayCount: number; revenueToday: number; lowStockItems: InventoryItem[]; recentOrders: Order[] }
interface MenuIndex { menuItems: MenuItem[]; categories: string[] }

@Component({ selector: 'app-root', standalone: true, imports: [RouterLink, RouterOutlet], template: `<header><div class="shell header-inner"><a routerLink="/" class="brand">FnB Management</a><nav><a routerLink="/">Dashboard</a><a routerLink="/menu">Menu</a><a routerLink="/inventory">Inventory</a></nav></div></header><main class="shell"><router-outlet /></main>` })
export class App {}

@Component({ selector: 'app-dashboard', standalone: true, imports: [CommonModule, RouterLink], template: `
  <div class="heading"><div><h1>Dashboard</h1><p>Overview of today's operations.</p></div></div>
  @if (error) { <p class="error">{{ error }}</p> }
  @if (data) {
    <section class="kpis">
      <article><span>Menu items</span><strong>{{data.menuItemsCount}}</strong></article><article><span>Available</span><strong>{{data.availableMenuItemsCount}}</strong></article>
      <article><span>Inventory SKUs</span><strong>{{data.inventoryItemsCount}}</strong></article><article><span>Low stock</span><strong>{{data.lowStockItemsCount}}</strong></article>
      <article><span>Orders today</span><strong>{{data.ordersTodayCount}}</strong></article><article><span>Revenue today</span><strong>{{data.revenueToday | currency}}</strong></article>
    </section>
    <section class="panels"><article class="panel"><h2>Low stock alerts</h2>@for (item of data.lowStockItems; track item.id) { <p><a routerLink="/inventory">{{item.name}}</a>: {{item.quantityInStock}} {{item.unit}} (reorder at {{item.reorderLevel}})</p> } @empty { <p>No low stock items.</p> }</article>
    <article class="panel"><h2>Recent orders</h2>@for (order of data.recentOrders; track order.id) { <p>#{{order.id}} · {{order.orderedAtUtc | date:'medium'}} · {{order.total | currency}}</p> } @empty { <p>No recent orders.</p> }</article></section>
  }` })
export class DashboardPage implements OnInit {
  private http = inject(HttpClient);
  data?: Dashboard;
  error = '';
  ngOnInit() { this.http.get<Dashboard>('/api/dashboard').subscribe({ next: data => this.data = data, error: () => this.error = 'Could not load dashboard.' }); }
}

@Component({ selector: 'app-menu', standalone: true, imports: [CommonModule, FormsModule], template: `
  <div class="heading"><div><h1>Menu items</h1><p>Manage prices, categories, and availability.</p></div><button (click)="startCreate()">Add menu item</button></div>
  @if (error) { <p class="error">{{error}}</p> }
  <form class="filters" (ngSubmit)="load()"><label>Search <input name="search" [(ngModel)]="search" placeholder="Item name"></label><label>Category <select name="category" [(ngModel)]="category"><option value="">All categories</option>@for (c of categories; track c) { <option [value]="c">{{c}}</option> }</select></label><button type="submit">Apply filters</button><button type="button" class="secondary" (click)="clear()">Clear</button></form>
  <div class="panel table-wrap"><table><thead><tr><th>Name</th><th>Category</th><th>Price</th><th>Status</th><th>Actions</th></tr></thead><tbody>@for (item of items; track item.id) { <tr><td>{{item.name}}</td><td>{{item.category}}</td><td>{{item.price | currency}}</td><td>{{item.isAvailable ? 'Available' : 'Archived'}}</td><td><button class="text" (click)="edit(item)">Edit</button> @if (item.isAvailable) { <button class="text" (click)="archive(item)">Archive</button> }</td></tr> } @empty { <tr><td colspan="5">No menu items match the filters.</td></tr> }</tbody></table></div>
  @if (form) { <section class="panel editor"><h2>{{form.id ? 'Edit' : 'Add'}} menu item</h2><form (ngSubmit)="save()" #menuForm="ngForm"><label>Name <input name="name" [(ngModel)]="form.name" required minlength="2" maxlength="160"></label><label>Category <input name="itemCategory" [(ngModel)]="form.category" required minlength="2" maxlength="80"></label><label>Price <input name="price" type="number" [(ngModel)]="form.price" required min="0.01" step="0.01"></label><label class="check"><input name="available" type="checkbox" [(ngModel)]="form.isAvailable"> Available for ordering</label><div class="actions"><button [disabled]="menuForm.invalid || saving">Save</button><button type="button" class="secondary" (click)="form = undefined">Cancel</button></div></form></section> }
` })
export class MenuPage implements OnInit {
  private http = inject(HttpClient);
  items: MenuItem[] = []; categories: string[] = []; search = ''; category = ''; form?: MenuItem; error = ''; saving = false;
  ngOnInit() { this.load(); }
  load() { let params = new HttpParams().set('searchTerm', this.search).set('category', this.category); this.http.get<MenuIndex>('/api/menu', { params }).subscribe({next: data => { this.items = data.menuItems; this.categories = data.categories; this.error = ''; }, error: () => this.error = 'Could not load menu.'}); }
  clear() { this.search = ''; this.category = ''; this.load(); }
  startCreate() { this.form = { id: 0, name: '', category: '', price: 0.01, isAvailable: true }; }
  edit(item: MenuItem) { this.form = { ...item }; }
  save() { if (!this.form) return; this.saving = true; const item = this.form; const request = item.id ? this.http.put(`/api/menu/${item.id}`, item) : this.http.post('/api/menu', item); request.subscribe({next: () => { this.saving = false; this.form = undefined; this.load(); }, error: () => { this.saving = false; this.error = 'Could not save menu item.'; }}); }
  archive(item: MenuItem) { this.http.post(`/api/menu/${item.id}/archive`, {}).subscribe({next: () => this.load(), error: () => this.error = 'Could not archive menu item.'}); }
}

@Component({ selector: 'app-inventory', standalone: true, imports: [CommonModule, FormsModule], template: `
  <div class="heading"><div><h1>Inventory</h1><p>Track stock and reorder needs.</p></div><button (click)="startCreate()">Add inventory item</button></div>
  @if (error) { <p class="error">{{error}}</p> }
  <form class="filters" (ngSubmit)="load()"><label>Search <input name="search" [(ngModel)]="search" placeholder="Item name"></label><label class="check"><input type="checkbox" name="lowStockOnly" [(ngModel)]="lowStockOnly"> Low stock only</label><button>Apply filters</button><button type="button" class="secondary" (click)="clear()">Clear</button></form>
  <div class="panel table-wrap"><table><thead><tr><th>Name</th><th>Quantity</th><th>Unit</th><th>Reorder level</th><th>Status</th><th>Actions</th></tr></thead><tbody>@for (item of items; track item.id) { <tr><td>{{item.name}}</td><td>{{item.quantityInStock | number:'1.2-2'}}</td><td>{{item.unit}}</td><td>{{item.reorderLevel | number:'1.2-2'}}</td><td>{{item.isLowStock ? 'Low stock' : 'In stock'}}</td><td><button class="text" (click)="edit(item)">Edit</button><button class="text" (click)="adjust(item)">Adjust stock</button>@if (item.isLowStock) { <button class="text" (click)="reorder(item)">Reorder</button> }</td></tr> } @empty { <tr><td colspan="6">No inventory items match the filters.</td></tr> }</tbody></table></div>
  @if (form) { <section class="panel editor"><h2>{{form.id ? 'Edit' : 'Add'}} inventory item</h2><form (ngSubmit)="save()" #inventoryForm="ngForm"><label>Name <input name="name" [(ngModel)]="form.name" required minlength="2" maxlength="160"></label><label>Unit <input name="unit" [(ngModel)]="form.unit" required maxlength="40"></label><label>Quantity in stock <input name="quantity" type="number" [(ngModel)]="form.quantityInStock" required min="0" step="0.01"></label><label>Reorder level <input name="reorderLevel" type="number" [(ngModel)]="form.reorderLevel" required min="0" step="0.01"></label><div class="actions"><button [disabled]="inventoryForm.invalid || saving">Save</button><button type="button" class="secondary" (click)="form = undefined">Cancel</button></div></form></section> }
  @if (adjusting) { <section class="panel editor"><h2>Adjust {{adjusting.name}}</h2><form (ngSubmit)="saveAdjustment()" #adjustForm="ngForm"><label>Change in stock (negative to subtract) <input type="number" name="change" [(ngModel)]="change" required step="0.01"></label><div class="actions"><button [disabled]="adjustForm.invalid || saving">Apply</button><button type="button" class="secondary" (click)="adjusting = undefined">Cancel</button></div></form></section> }
` })
export class InventoryPage implements OnInit {
  private http = inject(HttpClient);
  items: InventoryItem[] = []; search = ''; lowStockOnly = false; form?: InventoryItem; adjusting?: InventoryItem; change = 0; error = ''; saving = false;
  ngOnInit() { this.load(); }
  load() { const params = new HttpParams().set('searchTerm', this.search).set('lowStockOnly', this.lowStockOnly); this.http.get<InventoryItem[]>('/api/inventory', {params}).subscribe({next: items => { this.items = items; this.error = ''; }, error: () => this.error = 'Could not load inventory.'}); }
  clear() { this.search = ''; this.lowStockOnly = false; this.load(); }
  startCreate() { this.form = {id: 0, name: '', unit: '', quantityInStock: 0, reorderLevel: 0, isLowStock: true}; }
  edit(item: InventoryItem) { this.form = {...item}; }
  save() { if (!this.form) return; this.saving = true; const item = this.form; const request = item.id ? this.http.put(`/api/inventory/${item.id}`, item) : this.http.post('/api/inventory', item); request.subscribe({next: () => {this.saving = false; this.form = undefined; this.load();}, error: () => {this.saving = false; this.error = 'Could not save inventory item.';}}); }
  adjust(item: InventoryItem) { this.adjusting = item; this.change = 0; }
  saveAdjustment() { if (!this.adjusting) return; this.saving = true; this.http.post(`/api/inventory/${this.adjusting.id}/adjust`, {quantity: this.change}).subscribe({next: () => {this.saving = false; this.adjusting = undefined; this.load();}, error: () => {this.saving = false; this.error = 'Could not adjust stock.';}}); }
  reorder(item: InventoryItem) { const quantity = Math.max(1, item.reorderLevel - item.quantityInStock + 1); this.http.post(`/api/inventory/${item.id}/adjust`, {quantity}).subscribe({next: () => this.load(), error: () => this.error = 'Could not reorder stock.'}); }
}
