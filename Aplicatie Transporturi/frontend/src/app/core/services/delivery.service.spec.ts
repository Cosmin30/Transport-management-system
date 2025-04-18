import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { DeliveryService } from './delivery.service';
import { environment } from 'src/environments/environment';

describe('DeliveryService', () => {
  let service: DeliveryService;
  let httpMock: HttpTestingController;
  const baseUrl = `${environment.apiUrl}/deliveries`;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [DeliveryService]
    });
    service = TestBed.inject(DeliveryService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should fetch all deliveries', () => {
    const dummyDeliveries = [{ id: 1, pickupLocation: 'A', dropoffLocation: 'B' }];
    service.getDeliveries().subscribe((data) => {
      expect(data).toEqual(dummyDeliveries);
    });

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('GET');
    req.flush(dummyDeliveries);
  });

  it('should fetch a delivery by ID', () => {
    const delivery = { id: 1, pickupLocation: 'X', dropoffLocation: 'Y' };

    service.getDeliveryById(1).subscribe((data) => {
      expect(data).toEqual(delivery);
    });

    const req = httpMock.expectOne(`${baseUrl}/1`);
    expect(req.request.method).toBe('GET');
    req.flush(delivery);
  });

  it('should add a delivery', () => {
    const newDelivery = { pickupLocation: 'X', dropoffLocation: 'Y' };

    service.addDelivery(newDelivery).subscribe((res) => {
      expect(res).toEqual(newDelivery);
    });

    const req = httpMock.expectOne(baseUrl);
    expect(req.request.method).toBe('POST');
    req.flush(newDelivery);
  });

  it('should update a delivery', () => {
    const updatedDelivery = { id: 1, pickupLocation: 'A', dropoffLocation: 'B' };

    service.updateDelivery(1, updatedDelivery).subscribe((res) => {
      expect(res).toEqual(updatedDelivery);
    });

    const req = httpMock.expectOne(`${baseUrl}/1`);
    expect(req.request.method).toBe('PUT');
    req.flush(updatedDelivery);
  });

  it('should delete a delivery', () => {
    service.deleteDelivery(1).subscribe((res) => {
      expect(res).toBeTruthy();
    });

    const req = httpMock.expectOne(`${baseUrl}/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush({ success: true });
  });
});
