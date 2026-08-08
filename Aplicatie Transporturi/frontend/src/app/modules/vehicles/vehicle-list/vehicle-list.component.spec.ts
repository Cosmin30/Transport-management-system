import { ComponentFixture, TestBed } from '@angular/core/testing';
import { VehicleListComponent } from './vehicle-list.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { provideRouter } from '@angular/router';
import { VehicleService } from 'src/app/core/services/vehicle.service';
import { of, throwError } from 'rxjs';

describe('VehicleListComponent', () => {
  let component: VehicleListComponent;
  let fixture: ComponentFixture<VehicleListComponent>;
  let mockVehicleService: jasmine.SpyObj<VehicleService>;

  beforeEach(async () => {
    mockVehicleService = jasmine.createSpyObj('VehicleService', ['getVehicles']);

    await TestBed.configureTestingModule({
      imports: [VehicleListComponent, HttpClientTestingModule],
      providers: [
        provideRouter([]),
        { provide: VehicleService, useValue: mockVehicleService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(VehicleListComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load vehicles on init', () => {
    const mockVehicles = [
      { id: 1, licensePlate: 'B123ABC', model: 'VW Golf', year: 2020, isAvailable: true },
      { id: 2, licensePlate: 'CJ456XYZ', model: 'Dacia Duster', year: 2022, isAvailable: false }
    ];

    mockVehicleService.getVehicles.and.returnValue(of(mockVehicles));

    fixture.detectChanges();

    expect(component.vehicles.length).toBe(2);
    expect(component.vehicles[0].licensePlate).toBe('B123ABC');
    expect(mockVehicleService.getVehicles).toHaveBeenCalled();
  });

  it('should handle error on vehicle load', () => {
    spyOn(console, 'error');
    mockVehicleService.getVehicles.and.returnValue(throwError(() => new Error('Eroare test')));

    fixture.detectChanges();

    expect(console.error).toHaveBeenCalled();
    expect(component.vehicles.length).toBe(0);
  });
});
