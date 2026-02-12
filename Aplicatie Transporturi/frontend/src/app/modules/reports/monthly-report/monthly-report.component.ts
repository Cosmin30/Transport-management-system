import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DeliveryService } from 'src/app/core/services/delivery.service';
import { MonthlyReport } from 'src/app/core/models/delivery.model';

@Component({
  selector: 'app-monthly-report',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './monthly-report.component.html',
  styleUrls: ['./monthly-report.component.css']
})
export class MonthlyReportComponent implements OnInit {
  monthlyData: MonthlyReport[] = [];
  loading = true;
  error = '';

  // Summary stats
  totalRevenue = 0;
  totalProfit = 0;
  totalDeliveries = 0;
  avgMonthlyRevenue = 0;
  avgMonthlyProfit = 0;

  constructor(private deliveryService: DeliveryService) {}

  ngOnInit(): void {
    this.loadMonthlyReport();
  }

  loadMonthlyReport(): void {
    this.loading = true;
    this.deliveryService.getMonthlyReport().subscribe({
      next: (data) => {
        this.monthlyData = data;
        this.calculateSummary();
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading monthly report:', err);
        this.error = 'Nu s-a putut încărca raportul lunar';
        this.loading = false;
      }
    });
  }

  calculateSummary(): void {
    this.totalRevenue = this.monthlyData.reduce((sum, m) => sum + m.totalRevenue, 0);
    this.totalProfit = this.monthlyData.reduce((sum, m) => sum + m.totalProfit, 0);
    this.totalDeliveries = this.monthlyData.reduce((sum, m) => sum + m.totalDeliveries, 0);
    
    if (this.monthlyData.length > 0) {
      this.avgMonthlyRevenue = this.totalRevenue / this.monthlyData.length;
      this.avgMonthlyProfit = this.totalProfit / this.monthlyData.length;
    }
  }

  getMonthName(month: number): string {
    const months = [
      'Ianuarie', 'Februarie', 'Martie', 'Aprilie', 'Mai', 'Iunie',
      'Iulie', 'August', 'Septembrie', 'Octombrie', 'Noiembrie', 'Decembrie'
    ];
    return months[month - 1] || '';
  }

  getBarHeight(value: number, maxValue: number): number {
    if (maxValue === 0) return 0;
    return (value / maxValue) * 100;
  }

  get maxRevenue(): number {
    return Math.max(...this.monthlyData.map(m => m.totalRevenue), 1);
  }

  get maxProfit(): number {
    return Math.max(...this.monthlyData.map(m => Math.abs(m.totalProfit)), 1);
  }

  abs(value: number): number {
    return Math.abs(value);
  }
}
