using AutoFy.Core.Models;
using AutoFy.Services.DTOs;

namespace AutoFy.Services.Interfaces;

public interface IFuelService
{
    Task AddFuelEntryAsync(FuelEntry fuelEntry);

    Task<IEnumerable<FuelEntryDto>> GetFuelEntriesByVehicleIdAsync(int vehicleId);

    decimal CalculateConsumption(decimal liters, int distance);

    decimal CalculateCostPerKilometer(decimal totalPrice, int distance);

    Task<decimal> GetTotalFuelLitersAsync(int vehicleId);

    Task<decimal> GetTotalFuelCostAsync(int vehicleId);

    Task<decimal> GetAverageConsumptionAsync(int vehicleId);

    Task<decimal?> GetLastConsumptionAsync(int vehicleId);
}