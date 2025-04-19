import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router } from '@angular/router';
import { VehicleService } from 'src/app/core/services/vehicle.service';

@Component({
  selector: 'app-vehicle-list',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: any[] = [];
  error: string | null = null;
  loading = true;
  userId: string | null = null;

  constructor(private vehicleService: VehicleService, private router: Router) {}

  ngOnInit(): void {
    this.loadVehicles();
  }
  
  loadVehicles(): void {
    this.vehicleService.getVehicles().subscribe({
      next: data => {
        this.vehicles = data;
        this.loading = false;
      },
      error: err => {
        console.error('Eroare la încărcare:', err);
        this.error = 'Nu s-au putut încărca vehiculele.';
        this.loading = false;
      }
    });
  }
  

  editVehicle(id: number): void {
    this.router.navigate(['/vehicles/edit', id]);
  }

  deleteVehicle(id: number): void {
    if (confirm('Ești sigur că vrei să ștergi acest vehicul?')) {
      this.vehicleService.deleteVehicle(id).subscribe(() => {
        this.loadVehicles();
      });
    }
  }
}
