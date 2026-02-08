import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeliveryService } from 'src/app/core/services/delivery.service';
import { VehicleUtilization } from 'src/app/core/models/delivery.model';

@Component({
  selector: 'app-vehicle-utilization',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './vehicle-utilization.component.html',
  styleUrls: ['./vehicle-utilization.component.css']
})
export class VehicleUtilizationComponent implements OnInit {
  vehicles: VehicleUtilization[] = [];
  loading = true;
  error = '';

  constructor(private deliveryService: DeliveryService) {}

  ngOnInit(): void {
    this.loadVehicleUtilization();
  }

  loadVehicleUtilization(): void {
    this.deliveryService.getVehicleUtilization().subscribe({
      next: (data) => {
        this.vehicles = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading vehicle utilization:', err);
        this.error = 'Nu s-au putut încărca datele vehiculelor';
        this.loading = false;
      }
    });
  }

  getStatusClass(isAvailable: boolean): string {
    return isAvailable ? 'status-available' : 'status-in-use';
  }

  getStatusText(isAvailable: boolean): string {
    return isAvailable ? 'Disponibil' : 'În uz';
  }
}
