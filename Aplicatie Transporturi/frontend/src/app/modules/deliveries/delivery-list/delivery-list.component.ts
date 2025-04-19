import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-delivery-list',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './delivery-list.component.html',
  styleUrls: ['./delivery-list.component.css']
  
})
export class DeliveryListComponent implements OnInit {
  deliveries: any[] = [];
  loading: boolean = true;
  error: string | null = null;

  private apiUrl = environment.apiUrl + '/deliveries';
  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.fetchDeliveries();
  }

  fetchDeliveries(): void {
    this.loading = true;
    this.error = null;

    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.deliveries = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Eroare la preluarea livrărilor:', err);
        this.error = 'Nu s-au putut încărca cursele.';
        this.loading = false;
      }
    });
  }

  editDelivery(id: number): void {
    this.router.navigate(['/deliveries/edit', id]);
  }

  deleteDelivery(id: number): void {
    if (!confirm('Sigur vrei să ștergi această cursă?')) return;

    this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => {
        this.fetchDeliveries(); // refresh list
      },
      error: (err) => {
        console.error('Eroare la ștergere:', err);
        this.error = 'Nu s-a putut șterge cursa.';
      }
    });
  }
}
