import { Routes } from '@angular/router';
import { VehicleListComponent } from './modules/vehicles/vehicle-list/vehicle-list.component';
import { DriverListComponent } from './modules/drivers/driver-list/driver-list.component';
import { DeliveryListComponent } from './modules/deliveries/delivery-list/delivery-list.component';
import { LoginRegisterComponent } from './modules/auth/login-register/login-register.component';

export const routes: Routes = [
  // Static page routes (standalone)
  {
    path: 'about',
    loadComponent: () =>
      import('./modules/about/about/about.component').then(m => m.AboutComponent)
  },
  {
    path: 'services',
    loadComponent: () =>
      import('./modules/services/services/services.component').then(m => m.ServicesComponent)
  },
  {
    path: 'contact',
    loadComponent: () =>
      import('./modules/contact/contact/contact.component').then(m => m.ContactComponent)
  },
  {
    path: 'support',
    loadComponent: () =>
      import('./modules/support/support/support.component').then(m => m.SupportComponent)
  },

  // App core routes
  {
    path: '',
    loadComponent: () =>
      import('./modules/home/home/home.component').then(m => m.HomeComponent)
  },
  { path: 'auth', component: LoginRegisterComponent },

  // Entities
  { path: 'vehicles', component: VehicleListComponent },
  { path: 'drivers', component: DriverListComponent },
  { path: 'deliveries', component: DeliveryListComponent },

  // Form routes (lazy-loaded)
  {
    path: 'deliveries/add',
    loadComponent: () =>
      import('./modules/deliveries/delivery-form.component').then(m => m.DeliveryFormComponent)
  },
  {
    path: 'deliveries/edit/:id',
    loadComponent: () =>
      import('./modules/deliveries/delivery-form.component').then(m => m.DeliveryFormComponent)
  },
  {
    path: 'drivers/add',
    loadComponent: () =>
      import('./modules/drivers/driver-form.component').then(m => m.DriverFormComponent)
  },
  {
    path: 'drivers/edit/:id',
    loadComponent: () =>
      import('./modules/drivers/driver-form.component').then(m => m.DriverFormComponent)
  },
  {
    path: 'vehicles/add',
    loadComponent: () =>
      import('./modules/vehicles/vehicle-form.component').then(m => m.VehicleFormComponent)
  },
  {
    path: 'vehicles/edit/:id',
    loadComponent: () =>
      import('./modules/vehicles/vehicle-form.component').then(m => m.VehicleFormComponent)
  },

  // Wildcard fallback
  { path: '**', redirectTo: '', pathMatch: 'full' }
];
