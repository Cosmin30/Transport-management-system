// src/app/core/services/vehicle.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private baseUrl = environment.apiUrl + '/vehicles';

  constructor(private http: HttpClient) {}

  // 🔹 GET toate vehiculele pentru user
  getVehicles(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl); // fără userId
  }

  // 🔹 GET un vehicul după ID
  getVehicleById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  // 🔹 POST - adaugă vehicul
  addVehicle(vehicle: any): Observable<any> {
    return this.http.post(this.baseUrl, vehicle); // fără userId
  }
  // 🔹 PUT - actualizează vehicul
  updateVehicle(id: number, vehicle: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, vehicle);
  }

  // 🔹 DELETE - șterge vehicul
  deleteVehicle(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
