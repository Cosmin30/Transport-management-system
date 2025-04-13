import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HomeService {
  private baseUrl = 'https://localhost:5001/api'; // adaptează dacă e alt port

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
