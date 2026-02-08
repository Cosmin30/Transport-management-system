import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { 
  Delivery, 
  LocationUpdate, 
  DeliveryReport, 
  DriverPerformance, 
  VehicleUtilization, 
  MonthlyReport,
  FinancialOverview 
} from '../models/delivery.model';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {
  private baseUrl = environment.apiUrl + '/deliveries';
  private reportsUrl = environment.apiUrl + '/reports';

  constructor(private http: HttpClient) {}

  // CRUD Operations
  getDeliveries(): Observable<Delivery[]> {
    return this.http.get<Delivery[]>(this.baseUrl);
  }

  getDeliveryById(id: number): Observable<Delivery> {
    return this.http.get<Delivery>(`${this.baseUrl}/${id}`);
  }

  addDelivery(delivery: any): Observable<any> {
    return this.http.post(this.baseUrl, delivery);
  }

  updateDelivery(id: number, delivery: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, delivery);
  }

  deleteDelivery(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  // Status Management
  updateStatus(id: number, newStatus: string): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${id}/status`, JSON.stringify(newStatus), {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  // GPS Tracking
  updateLocation(id: number, location: LocationUpdate): Observable<any> {
    return this.http.patch(`${this.baseUrl}/${id}/location`, location);
  }

  getLocation(id: number): Observable<{ latitude: number; longitude: number; lastUpdate: Date }> {
    return this.http.get<any>(`${this.baseUrl}/${id}/location`);
  }

  // Complete Delivery
  completeDelivery(id: number, actualCost: number): Observable<{ message: string; profit: number }> {
    return this.http.post<any>(`${this.baseUrl}/${id}/complete`, actualCost, {
      headers: { 'Content-Type': 'application/json' }
    });
  }

  // Reports
  getDeliverySummary(): Observable<DeliveryReport> {
    return this.http.get<DeliveryReport>(`${this.reportsUrl}/delivery-summary`);
  }

  getDriverPerformance(): Observable<DriverPerformance[]> {
    return this.http.get<DriverPerformance[]>(`${this.reportsUrl}/driver-performance`);
  }

  getVehicleUtilization(): Observable<VehicleUtilization[]> {
    return this.http.get<VehicleUtilization[]>(`${this.reportsUrl}/vehicle-utilization`);
  }

  getMonthlyReport(): Observable<MonthlyReport[]> {
    return this.http.get<MonthlyReport[]>(`${this.reportsUrl}/monthly`);
  }

  getFinancialOverview(): Observable<FinancialOverview> {
    return this.http.get<FinancialOverview>(`${this.reportsUrl}/financial-overview`);
  }
}

