import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { DashboardPage } from './dashboard-page';

describe('Dashboard low-stock alerts', () => {
  it('shows Milk below reorder level and omits Coffee Beans above it', async () => {
    TestBed.configureTestingModule({
      imports: [DashboardPage],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter([])],
    });
    const fixture = TestBed.createComponent(DashboardPage);
    fixture.detectChanges();

    TestBed.inject(HttpTestingController).expectOne('/api/dashboard').flush({
      menuItemsCount: 0,
      availableMenuItemsCount: 0,
      inventoryItemsCount: 2,
      lowStockItemsCount: 1,
      ordersTodayCount: 0,
      revenueToday: 0,
      lowStockItems: [
        { id: 1, name: 'Milk', unit: 'units', quantityInStock: 2, reorderLevel: 5, isLowStock: true },
      ],
      recentOrders: [],
    });
    await fixture.whenStable();
    fixture.detectChanges();

    expect(fixture.componentInstance.data()).toBeTruthy();
    const alerts = fixture.nativeElement.querySelector('.panels article') as HTMLElement;
    expect(alerts.textContent).toContain('Milk: 2 units in stock (reorder at 5 units)');
    expect(alerts.textContent).not.toContain('Coffee Beans');
    TestBed.inject(HttpTestingController).verify();
  });
});
