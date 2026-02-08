import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DeliveryService } from 'src/app/core/services/delivery.service';

@Component({
  selector: 'app-delivery-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './delivery-form.component.html',
  styleUrls: ['./delivery-form.component.css']
})
export class DeliveryFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  deliveryId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private deliveryService: DeliveryService
  ) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      pickupLocation: ['', Validators.required],
      dropoffLocation: ['', Validators.required],
      scheduledDate: ['', Validators.required],
      status: ['Planned', Validators.required],
      driverId: [null],
      vehicleId: [null],
      estimatedCost: [0, [Validators.required, Validators.min(0)]],
      revenue: [0, [Validators.required, Validators.min(0)]],
      distanceKm: [0, [Validators.required, Validators.min(1)]],
      notes: ['']
    });
  
    const id = this.route.snapshot.paramMap.get('id');
    console.log('ID din URL:', id); 
  
    if (id) {
      this.isEditMode = true;
      this.deliveryId = +id;
  
      this.deliveryService.getDeliveryById(this.deliveryId).subscribe(data => {
        console.log('Date primite pentru edit:', data); 
  
        const formattedDate = new Date(data.scheduledDate).toISOString().slice(0, 16);
        this.form.patchValue({
          ...data,
          scheduledDate: formattedDate
        });
      });
    }
  }
  

  onSubmit(): void {
    console.log('>>> Submit apăsat');
  
    if (this.form.invalid) {
      console.warn('Formular invalid:', this.form.value);
      return;
    }
  
    const delivery = { ...this.form.value };
  
    if (this.isEditMode && this.deliveryId !== null) {
      delivery.id = this.deliveryId;
  
      console.log('>>> Trimitem la update:', delivery);
  
      this.deliveryService.updateDelivery(this.deliveryId, delivery).subscribe(() => {
        this.router.navigate(['/deliveries']);
      });
    } else {
      console.log('>>> Trimitem la adăugare:', delivery);
  
      this.deliveryService.addDelivery(delivery).subscribe(() => {
        this.router.navigate(['/deliveries']);
      });
    }
  }
  
  
  
}
