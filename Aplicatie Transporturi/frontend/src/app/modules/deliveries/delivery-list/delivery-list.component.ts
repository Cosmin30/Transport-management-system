import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-delivery-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delivery-list.component.html',
  styleUrls: ['./delivery-list.component.css']
})
export class DeliveryListComponent implements OnInit {
  deliveries: any[] = [];
  loading = true;
  error: string | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<any[]>('https://localhost:5001/api/deliveries').subscribe({
      next: (data) => {
        this.deliveries = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Eroare:', err);
        this.error = 'Eroare la încărcarea curselor.';
        this.loading = false;
      }
    });
  }
}
