using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aplicatie_Transporturi.Migrations
{
    /// <inheritdoc />
    public partial class AddTrackingAndFinancialFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FuelConsumptionPer100Km",
                table: "Vehicles",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastMaintenanceDate",
                table: "Vehicles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalKmDriven",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalMaintenanceCost",
                table: "Vehicles",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDeliveryDate",
                table: "Drivers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalDeliveriesCompleted",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalKmDriven",
                table: "Drivers",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ActualCost",
                table: "Deliveries",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLatitude",
                table: "Deliveries",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLongitude",
                table: "Deliveries",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistanceKm",
                table: "Deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "EstimatedCost",
                table: "Deliveries",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FuelCost",
                table: "Deliveries",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLocationUpdate",
                table: "Deliveries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Deliveries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Revenue",
                table: "Deliveries",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FuelConsumptionPer100Km",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "LastMaintenanceDate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TotalKmDriven",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "TotalMaintenanceCost",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "LastDeliveryDate",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "TotalDeliveriesCompleted",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "TotalKmDriven",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "ActualCost",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CurrentLatitude",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "CurrentLongitude",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "DistanceKm",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "EstimatedCost",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "FuelCost",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "LastLocationUpdate",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "Revenue",
                table: "Deliveries");
        }
    }
}
