import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DeliveryService } from 'src/app/core/services/delivery.service';
import { DeliveryReport, FinancialOverview } from 'src/app/core/models/delivery.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  deliveryReport?: DeliveryReport;
  financialOverview?: FinancialOverview;
  loading = true;
  error = '';

  constructor(private deliveryService: DeliveryService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;
    
    this.deliveryService.getDeliverySummary().subscribe({
      next: (data) => {
        this.deliveryReport = data;
      },
      error: (err) => {
        console.error('Error loading delivery summary:', err);
        this.error = 'Failed to load delivery summary';
      }
    });

    this.deliveryService.getFinancialOverview().subscribe({
      next: (data) => {
        this.financialOverview = data;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading financial overview:', err);
        this.error = 'Failed to load financial overview';
        this.loading = false;
      }
    });
  }

  get completionRate(): number {
    if (!this.deliveryReport || this.deliveryReport.totalDeliveries === 0) return 0;
    return (this.deliveryReport.completedDeliveries / this.deliveryReport.totalDeliveries) * 100;
  }
}
