import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { InventoryItem } from './inventory.model';

@Component({ selector: 'app-inventory', standalone: true, imports: [CommonModule, FormsModule], templateUrl: './inventory-page.html' })
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
