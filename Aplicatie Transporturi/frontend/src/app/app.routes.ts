import { Routes } from '@angular/router';
import { HomeComponent } from './modules/home/home/home.component';
import { VehicleListComponent } from './modules/vehicles/vehicle-list/vehicle-list.component';
import { DriverListComponent } from './modules/drivers/driver-list/driver-list.component';
import { DeliveryListComponent } from './modules/deliveries/delivery-list/delivery-list.component';
import { LoginRegisterComponent } from './modules/auth/login-register/login-register.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'vehicles', component: VehicleListComponent },
  { path: 'drivers', component: DriverListComponent },
  { path: 'deliveries', component: DeliveryListComponent },
  { path: 'auth', component: LoginRegisterComponent }
];
