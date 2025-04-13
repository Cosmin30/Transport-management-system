import { Routes } from '@angular/router';
import { VehicleListComponent } from './modules/vehicles/vehicle-list/vehicle-list.component';
import { LoginRegisterComponent } from './modules/auth/login-register/login-register.component';

export const routes: Routes = [
  { path: '', component: VehicleListComponent },
  { path: 'auth', component: LoginRegisterComponent }
];
