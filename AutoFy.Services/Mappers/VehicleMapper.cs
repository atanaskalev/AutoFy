using AutoFy.Core.Models;
using AutoFy.Services.DTOs;

namespace AutoFy.Services.Mappers;

public static class VehicleMapper
{
    public static VehicleDto ToDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Id = vehicle.Id,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
            Year = vehicle.Year,
            LicensePlate = vehicle.LicensePlate,
            Vin = vehicle.Vin,
            Mileage = vehicle.Mileage,
            FuelType = vehicle.FuelType,
            TransmissionType = vehicle.TransmissionType,
            EngineSize = vehicle.EngineSize,
            HorsePower = vehicle.HorsePower,
            ImagePath = vehicle.ImagePath,
            TechnicalInspectionDate = vehicle.TechnicalInspectionDate,
            InsuranceDate = vehicle.InsuranceDate,
            VignetteDate = vehicle.VignetteDate,
            FireExtinguisherDate = vehicle.FireExtinguisherDate
        };
    }

    public static Vehicle ToEntity(VehicleDto dto)
    {
        return new Vehicle
        {
            Id = dto.Id,
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            LicensePlate = dto.LicensePlate,
            Vin = dto.Vin,
            Mileage = dto.Mileage,
            FuelType = dto.FuelType,
            TransmissionType = dto.TransmissionType,
            EngineSize = dto.EngineSize,
            HorsePower = dto.HorsePower,
            ImagePath = dto.ImagePath,
            TechnicalInspectionDate = dto.TechnicalInspectionDate,
            InsuranceDate = dto.InsuranceDate,
            VignetteDate = dto.VignetteDate,
            FireExtinguisherDate = dto.FireExtinguisherDate
        };
    }

    public static void UpdateEntity(Vehicle vehicle, VehicleDto dto)
    {
        vehicle.Brand = dto.Brand;
        vehicle.Model = dto.Model;
        vehicle.Year = dto.Year;
        vehicle.LicensePlate = dto.LicensePlate;
        vehicle.Vin = dto.Vin;
        vehicle.Mileage = dto.Mileage;
        vehicle.FuelType = dto.FuelType;
        vehicle.TransmissionType = dto.TransmissionType;
        vehicle.EngineSize = dto.EngineSize;
        vehicle.HorsePower = dto.HorsePower;
        vehicle.ImagePath = dto.ImagePath;
        vehicle.TechnicalInspectionDate = dto.TechnicalInspectionDate;
        vehicle.InsuranceDate = dto.InsuranceDate;
        vehicle.VignetteDate = dto.VignetteDate;
        vehicle.FireExtinguisherDate = dto.FireExtinguisherDate;
    }
}