import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';

import { DriverListComponent } from './driver-list.component';

describe('DriverListComponent', () => {
  let component: DriverListComponent;
  let fixture: ComponentFixture<DriverListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DriverListComponent],
      providers: [provideRouter([])]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DriverListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
