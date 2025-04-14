import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DriverService {
  baseUrl = environment.apiUrl + 'drivers';

  constructor(private http: HttpClient) {}

  getDrivers() {
    return this.http.get(this.baseUrl);
  }

  addDriver(driver: any) {
    return this.http.post(this.baseUrl, driver);
  }
}
