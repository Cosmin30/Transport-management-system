export interface Delivery {
  id: number;
  pickupLocation: string;
  dropoffLocation: string;
  scheduledDate: Date;
  status: string;
  
  // GPS Tracking
  currentLatitude?: number;
  currentLongitude?: number;
  lastLocationUpdate?: Date;
  
  // Financial
  estimatedCost: number;
  actualCost: number;
  fuelCost: number;
  revenue: number;
  profit: number;
  
  // Additional Info
  notes?: string;
  distanceKm: number;
  
  // Relationships
  vehicleId?: number;
  vehicle?: any;
  driverId?: number;
  driver?: any;
  userId: number;
}

export interface LocationUpdate {
  latitude: number;
  longitude: number;
}

export interface DeliveryReport {
  totalDeliveries: number;
  completedDeliveries: number;
  inProgressDeliveries: number;
  plannedDeliveries: number;
  totalRevenue: number;
  totalCosts: number;
  totalProfit: number;
  totalKmDriven: number;
  averageDeliveryProfit: number;
}

export interface DriverPerformance {
  driverId: number;
  driverName: string;
  totalDeliveries: number;
  totalKmDriven: number;
  lastDeliveryDate?: Date;
  isAvailable: boolean;
}

export interface VehicleUtilization {
  vehicleId: number;
  licensePlate: string;
  model: string;
  totalDeliveriesAssigned: number;
  totalKmDriven: number;
  totalMaintenanceCost: number;
  isAvailable: boolean;
}

export interface MonthlyReport {
  month: number;
  year: number;
  totalDeliveries: number;
  totalRevenue: number;
  totalProfit: number;
}

export interface FinancialOverview {
  totalRevenue: number;
  totalCosts: number;
  totalFuelCosts: number;
  totalProfit: number;
  profitMargin: number;
  averageRevenuePerDelivery: number;
  averageCostPerKm: number;
}
