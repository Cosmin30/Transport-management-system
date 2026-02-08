import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-driver-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.css']
})
export class DriverListComponent implements OnInit {
  drivers: any[] = [];
  loading: boolean = true;
  error: string | null = null;

  private apiUrl = environment.apiUrl + '/drivers';
  
  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.fetchDrivers();
  }

  fetchDrivers(): void {
    this.loading = true;
    this.error = null;

    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (data) => {
        this.drivers = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Eroare la preluarea șoferilor:', err);
        this.error = 'Nu s-au putut încărca șoferii.';
        this.loading = false;
      }
    });
  }

  editDriver(id: number): void {
    this.router.navigate(['/drivers/edit', id]);
  }

  deleteDriver(id: number): void {
    if (!confirm('Sigur vrei să ștergi acest șofer?')) return;

    this.http.delete(`${this.apiUrl}/${id}`).subscribe({
      next: () => {
        this.fetchDrivers(); 
      },
      error: (err) => {
        console.error('Eroare la ștergere:', err);
        this.error = 'Nu s-a putut șterge șoferul.';
      }
    });
  }
}
