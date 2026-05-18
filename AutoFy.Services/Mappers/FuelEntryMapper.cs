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
}