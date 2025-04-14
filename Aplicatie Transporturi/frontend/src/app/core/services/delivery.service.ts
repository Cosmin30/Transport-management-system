import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DeliveryService {
  baseUrl = environment.apiUrl + 'deliveries';

  constructor(private http: HttpClient) {}

  getDeliveries() {
    return this.http.get(this.baseUrl);
  }

  addDelivery(delivery: any) {
    return this.http.post(this.baseUrl, delivery);
  }
}
