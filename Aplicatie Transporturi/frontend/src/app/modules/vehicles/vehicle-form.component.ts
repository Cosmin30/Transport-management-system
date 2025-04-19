import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { VehicleService } from 'src/app/core/services/vehicle.service';

@Component({
  selector: 'app-vehicle-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  vehicleId: number | null = null;
  userId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private vehicleService: VehicleService
  ) {}

  ngOnInit(): void {
    if (typeof window !== 'undefined') {
      this.userId = localStorage.getItem('userId');
    }
  
    this.form = this.fb.group({
      licensePlate: ['', Validators.required],
      model: ['', Validators.required],
      year: [new Date().getFullYear(), [Validators.required, Validators.min(1900)]],
      isAvailable: [true]
    });
  
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.vehicleId = +id;
  
      this.vehicleService.getVehicleById(this.vehicleId).subscribe(data => {
        this.form.patchValue(data);
      });
    }
  }
  

  onSubmit(): void {
    if (this.form.invalid) {
      console.warn('Formular invalid:', this.form.value);
      return;
    }
  
    const vehicle = { ...this.form.value };
  
    if (this.isEditMode && this.vehicleId !== null) {
      vehicle.id = this.vehicleId;
      this.vehicleService.updateVehicle(this.vehicleId, vehicle).subscribe(() => {
        this.router.navigate(['/vehicles']);
      });
    } else {
      this.vehicleService.addVehicle(vehicle).subscribe(() => {
        this.router.navigate(['/vehicles']);
      });
    }
  }
  
  
}
