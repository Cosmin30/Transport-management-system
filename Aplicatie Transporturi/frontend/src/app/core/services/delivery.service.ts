import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {
  baseUrl = environment.apiUrl + 'deliveries';

  constructor(private http: HttpClient) {}

  getDeliveries(): Observable<any[]> {
    return this.http.get<any[]>(this.baseUrl);
  }

  getDeliveryById(id: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
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
}
