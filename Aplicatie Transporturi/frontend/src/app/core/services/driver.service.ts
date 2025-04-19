import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  baseUrl = environment.apiUrl + '/drivers';

  constructor(private http: HttpClient) {}

  getDrivers(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  getDriverById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }
  addDriver(driver: any): Observable<any> {
    return this.http.post(`${this.baseUrl}`, driver);
  }
  
  updateDriver(id: number, driver: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, driver);
  }

  deleteDriver(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
