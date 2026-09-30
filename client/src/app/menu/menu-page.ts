import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MenuIndex, MenuItem } from './menu.model';

@Component({ selector: 'app-menu', standalone: true, imports: [CommonModule, FormsModule], templateUrl: './menu-page.html' })
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
