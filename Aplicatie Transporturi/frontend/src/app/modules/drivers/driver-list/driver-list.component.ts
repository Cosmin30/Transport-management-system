import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DriverService } from 'src/app/core/services/driver.service';

@Component({
  selector: 'app-driver-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './driver-list.component.html',
  styleUrls: ['./driver-list.component.css']
})
export class DriverListComponent implements OnInit {
  drivers: any[] = [];
  error: string | null = null;
  loading = true;

  constructor(private driverService: DriverService) {}

  ngOnInit(): void {
    this.driverService.getDrivers().subscribe(
      (data) => {
        this.drivers = data as any[];
        this.loading = false;
      },
      (err) => {
        console.error('Eroare:', err);
        this.error = 'Eroare la încărcarea șoferilor';
        this.loading = false;
      }
    );
  }
}
