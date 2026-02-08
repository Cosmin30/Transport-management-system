import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeliveryService } from 'src/app/core/services/delivery.service';
import { DriverPerformance } from 'src/app/core/models/delivery.model';

@Component({
  selector: 'app-driver-performance',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './driver-performance.component.html',
  styleUrls: ['./driver-performance.component.css']
})
export class DriverPerformanceComponent implements OnInit {
  drivers: DriverPerformance[] = [];
  loading = true;
  error = '';

  constructor(private deliveryService: DeliveryService) {}

  ngOnInit(): void {
    this.loadDriverPerformance();
  }

  loadDriverPerformance(): void {
    this.deliveryService.getDriverPerformance().subscribe({
      next: (data) => {
        this.drivers = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading driver performance:', err);
        this.error = 'Nu s-au putut încărca datele șoferilor';
        this.loading = false;
      }
    });
  }

  getStatusClass(isAvailable: boolean): string {
    return isAvailable ? 'status-available' : 'status-busy';
  }

  getStatusText(isAvailable: boolean): string {
    return isAvailable ? 'Disponibil' : 'Ocupat';
  }
}
