import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { trigger, style, animate, transition } from '@angular/animations';
import { HomeService } from 'src/app/core/services/home.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
  animations: [
    trigger('fadeIn', [
      transition(':enter', [
        style({ opacity: 0, transform: 'translateY(30px)' }),
        animate('600ms ease-out', style({ opacity: 1, transform: 'translateY(0)' }))
      ])
    ])
  ]
})
export class HomeComponent implements OnInit {
  vehicles: any[] = [];
  drivers: any[] = [];
  deliveries: any[] = [];

  constructor(private homeService: HomeService) {}

  ngOnInit(): void {
    this.homeService.getVehicles().subscribe(data => this.vehicles = data as any[]);
    this.homeService.getDrivers().subscribe(data => this.drivers = data as any[]);
    this.homeService.getDeliveries().subscribe(data => this.deliveries = data as any[]);
  }
}