import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { Dashboard } from './dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard-page.html',
})
export class DashboardPage implements OnInit {
  private http = inject(HttpClient);
  data = signal<Dashboard | undefined>(undefined);
  error = '';
  ngOnInit() {
    this.http
      .get<Dashboard>('/api/dashboard')
      .subscribe({
        next: (data) => this.data.set(data),
        error: () => (this.error = 'Could not load dashboard.'),
      });
  }
}
