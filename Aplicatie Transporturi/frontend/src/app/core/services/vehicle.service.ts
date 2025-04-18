import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  baseUrl = environment.apiUrl + '/vehicles';

  constructor(private http: HttpClient) { }

  getVehicles() {
    return this.http.get(this.baseUrl);
  }

  addVehicle(vehicle: any) {
    return this.http.post(this.baseUrl, vehicle);
  }
}
