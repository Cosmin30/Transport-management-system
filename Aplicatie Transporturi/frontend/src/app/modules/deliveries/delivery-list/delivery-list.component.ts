import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeService } from 'src/app/core/services/home.service';

@Component({
  selector: 'app-delivery-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delivery-list.component.html',
  styleUrls: ['./delivery-list.component.css']
})
export class DeliveryListComponent implements OnInit {
  deliveries: any[] = [];

  constructor(private homeService: HomeService) {}

  ngOnInit(): void {
    this.homeService.getDeliveries().subscribe(data => {
      console.log('>>> CURSE PRIMITE:', data); // debug în consolă
      this.deliveries = data as any[];
    });
  }
}
