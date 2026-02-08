import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { DeliveryService } from 'src/app/core/services/delivery.service';
import { interval, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-delivery-tracking',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delivery-tracking.component.html',
  styleUrls: ['./delivery-tracking.component.css']
})
export class DeliveryTrackingComponent implements OnInit, OnDestroy {
  deliveryId!: number;
  currentLocation?: { latitude: number; longitude: number; lastUpdate: Date };
  loading = true;
  error = '';
  private trackingSubscription?: Subscription;

  constructor(
    private route: ActivatedRoute,
    private deliveryService: DeliveryService
  ) {}

  ngOnInit(): void {
    this.deliveryId = +this.route.snapshot.paramMap.get('id')!;
    this.startTracking();
  }

  ngOnDestroy(): void {
    this.stopTracking();
  }

  startTracking(): void {
    this.trackingSubscription = interval(30000)
      .pipe(
        switchMap(() => this.deliveryService.getLocation(this.deliveryId))
      )
      .subscribe({
        next: (location) => {
          this.currentLocation = location;
          this.loading = false;
        },
        error: (err) => {
          console.error('Error tracking delivery:', err);
          this.error = 'Nu s-a putut încărca locația';
          this.loading = false;
        }
      });

    // Initial load
    this.deliveryService.getLocation(this.deliveryId).subscribe({
      next: (location) => {
        this.currentLocation = location;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading initial location:', err);
        this.error = 'Nu s-a putut încărca locația';
        this.loading = false;
      }
    });
  }

  stopTracking(): void {
    if (this.trackingSubscription) {
      this.trackingSubscription.unsubscribe();
    }
  }

  refreshLocation(): void {
    this.loading = true;
    this.deliveryService.getLocation(this.deliveryId).subscribe({
      next: (location) => {
        this.currentLocation = location;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error refreshing location:', err);
        this.error = 'Nu s-a putut actualiza locația';
        this.loading = false;
      }
    });
  }

  openInMaps(): void {
    if (this.currentLocation) {
      const url = `https://www.google.com/maps?q=${this.currentLocation.latitude},${this.currentLocation.longitude}`;
      window.open(url, '_blank');
    }
  }
}
