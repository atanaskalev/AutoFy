using AutoFy.Core.Models;
using AutoFy.Services.DTOs;

namespace AutoFy.Services.Mappers;

public static class FuelEntryMapper
{
    public static FuelEntryDto ToDto(FuelEntry fuelEntry)
    {
        return new FuelEntryDto
        {
            Id = fuelEntry.Id,
            VehicleId = fuelEntry.VehicleId,
            Date = fuelEntry.Date,
            Odometer = fuelEntry.Odometer,
            Distance = fuelEntry.Distance,
            Liters = fuelEntry.Liters,
            PricePerLiter = fuelEntry.PricePerLiter,
            TotalPrice = fuelEntry.TotalPrice,
            Consumption = fuelEntry.Consumption,
            CostPerKilometer = fuelEntry.CostPerKilometer,
            Notes = fuelEntry.Notes
        };
    }

    public static void UpdateEntity(FuelEntry entity, FuelEntryDto dto)
    {
        entity.Date = dto.Date;
        entity.Odometer = dto.Odometer;
        entity.Distance = dto.Distance;
        entity.Liters = dto.Liters;
        entity.PricePerLiter = dto.PricePerLiter;
        entity.TotalPrice = dto.TotalPrice;
        entity.Consumption = dto.Consumption;
        entity.CostPerKilometer = dto.CostPerKilometer;
        entity.Notes = dto.Notes;
        entity.UpdatedAt = DateTime.Now;
    }
}