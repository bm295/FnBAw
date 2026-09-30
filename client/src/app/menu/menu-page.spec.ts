import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { MenuPage } from './menu-page';

describe('MenuPage', () => {
  it('starts with an empty available menu item', () => {
    TestBed.configureTestingModule({ providers: [provideHttpClient()] });
    const page = TestBed.runInInjectionContext(() => new MenuPage());

    page.startCreate();

    expect(page.form).toEqual({
      id: 0,
      name: '',
      category: '',
      price: 0.01,
      isAvailable: true,
    });
  });
});
