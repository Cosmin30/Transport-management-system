import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { DriverService } from 'src/app/core/services/driver.service';

@Component({
  selector: 'app-driver-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './driver-form.component.html',
  styleUrls: ['./driver-form.component.css']
})
export class DriverFormComponent implements OnInit {
  form!: FormGroup;
  isEditMode = false;
  driverId: number | null = null;
  userId: string | null = null;
  isLoading = false;
  error: string | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    public router: Router,
    private driverService: DriverService
  ) {}

  ngOnInit(): void {
    // preluăm userId din localStorage
    if (typeof window !== 'undefined') {
      this.userId = localStorage.getItem('userId');
    }

    // inițializăm formularul
    this.form = this.fb.group({
      name: ['', Validators.required],
      licenseNumber: ['', Validators.required],
      isAvailable: [true]
    });

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.driverId = +id;
  
      this.driverService.getDriverById(this.driverId).subscribe(data => {
        this.form.patchValue(data);
      });
    }
  }

  onSubmit(): void {
    if (this.form.invalid) {
      console.warn('Formular invalid:', this.form.value);
      return;
    }
  
    const driver = { ...this.form.value };
  
    if (this.isEditMode && this.driverId !== null) {
      driver.id = this.driverId;
      this.driverService.updateDriver(this.driverId, driver).subscribe(() => {
        this.router.navigate(['/drivers']);
      });
    } else {
      this.driverService.addDriver(driver).subscribe(() => {
        this.router.navigate(['/drivers']);
      });
    }
  }
}
