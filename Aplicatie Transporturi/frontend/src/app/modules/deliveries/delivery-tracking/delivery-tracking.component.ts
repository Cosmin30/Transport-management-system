import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { DeliveryService } from 'src/app/core/services/delivery.service';
import { interval, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Delivery } from 'src/app/core/models/delivery.model';

@Component({
  selector: 'app-delivery-tracking',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './delivery-tracking.component.html',
  styleUrls: ['./delivery-tracking.component.css']
})
export class DeliveryTrackingComponent implements OnInit, OnDestroy {
  deliveryId!: number;
  delivery?: Delivery;
  currentLocation?: { latitude: number; longitude: number; lastUpdate: Date };
  loading = true;
  error = '';
  private trackingSubscription?: Subscription;

  // Complete delivery
  showCompleteModal = false;
  actualCost = 0;
  completing = false;

  // Update location
  showLocationModal = false;
  newLatitude = 0;
  newLongitude = 0;
  updatingLocation = false;

  constructor(
    private route: ActivatedRoute,
    private deliveryService: DeliveryService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.deliveryId = +this.route.snapshot.paramMap.get('id')!;
    this.loadDelivery();
    this.startTracking();
  }

  ngOnDestroy(): void {
    this.stopTracking();
  }

  loadDelivery(): void {
    this.deliveryService.getDeliveryById(this.deliveryId).subscribe({
      next: (delivery) => {
        this.delivery = delivery;
      },
      error: (err) => {
        console.error('Error loading delivery:', err);
      }
    });
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

  // Complete Delivery Methods
  openCompleteModal(): void {
    this.actualCost = this.delivery?.estimatedCost || 0;
    this.showCompleteModal = true;
  }

  closeCompleteModal(): void {
    this.showCompleteModal = false;
  }

  confirmComplete(): void {
    this.completing = true;
    this.deliveryService.completeDelivery(this.deliveryId, this.actualCost).subscribe({
      next: (response) => {
        alert(`Cursă finalizată cu succes! Profit: ${response.profit.toFixed(2)} RON`);
        this.showCompleteModal = false;
        this.completing = false;
        this.router.navigate(['/deliveries']);
      },
      error: (err) => {
        console.error('Error completing delivery:', err);
        alert('Eroare la finalizarea cursei');
        this.completing = false;
      }
    });
  }

  // Update Location Methods
  openLocationModal(): void {
    this.newLatitude = this.currentLocation?.latitude || 0;
    this.newLongitude = this.currentLocation?.longitude || 0;
    this.showLocationModal = true;
  }

  closeLocationModal(): void {
    this.showLocationModal = false;
  }

  confirmLocationUpdate(): void {
    this.updatingLocation = true;
    this.deliveryService.updateLocation(this.deliveryId, {
      latitude: this.newLatitude,
      longitude: this.newLongitude
    }).subscribe({
      next: () => {
        this.refreshLocation();
        this.showLocationModal = false;
        this.updatingLocation = false;
      },
      error: (err) => {
        console.error('Error updating location:', err);
        alert('Eroare la actualizarea locației');
        this.updatingLocation = false;
      }
    });
  }

  getCurrentPosition(): void {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.newLatitude = position.coords.latitude;
          this.newLongitude = position.coords.longitude;
        },
        (error) => {
          console.error('Error getting position:', error);
          alert('Nu s-a putut obține locația GPS');
        }
      );
    }
  }
}
