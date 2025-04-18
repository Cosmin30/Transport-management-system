import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getVehicles() {
    return this.http.get(`${this.baseUrl}/vehicles`);
  }

  getDrivers() {
    return this.http.get(`${this.baseUrl}/drivers`);
  }

  getDeliveries() {
    return this.http.get(`${this.baseUrl}/deliveries`);
  }
}
