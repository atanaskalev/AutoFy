using AutoFy.Core.Enums;

namespace AutoFy.Services.DTOs;

public class VehicleDto
{
    public int Id { get; set; }

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }

    public string LicensePlate { get; set; } = string.Empty;

    public string Vin { get; set; } = string.Empty;

    public int Mileage { get; set; }

    public FuelType FuelType { get; set; }

    public TransmissionType TransmissionType { get; set; }

    public string EngineSize { get; set; } = string.Empty;

    public int HorsePower { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? TechnicalInspectionDate { get; set; }

    public DateTime? InsuranceDate { get; set; }

    public DateTime? VignetteDate { get; set; }

    public DateTime? FireExtinguisherDate { get; set; }

    public string DisplayName => $"{Brand} {Model}";

    public string ShortInfo => $"{Year} • {FuelType} • {Mileage} км";
}