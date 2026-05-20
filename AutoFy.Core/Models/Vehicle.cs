using AutoFy.Core.Base;
using AutoFy.Core.Enums;
using AutoFy.Core.Interfaces;

namespace AutoFy.Core.Models;

public class Vehicle : BaseEntity, IVehicle
{
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

    public decimal VignettePrice { get; set; }

    public decimal InsurancePrice { get; set; }

    public decimal TechnicalInspectionPrice { get; set; }

    public decimal FireExtinguisherPrice { get; set; }

    public DateTime? TechnicalInspectionDate { get; set; }

    public DateTime? InsuranceDate { get; set; }

    public DateTime? VignetteDate { get; set; }

    public DateTime? FireExtinguisherDate { get; set; }

    public ICollection<FuelEntry> FuelEntries { get; set; } = new List<FuelEntry>();

    public ICollection<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();

    public ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
}