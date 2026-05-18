using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using AutoFy.Services.Mappers;

namespace AutoFy.Services.Services;

public class FuelService : IFuelService
{
    private readonly IFuelEntryRepository fuelEntryRepository;

    public FuelService(IFuelEntryRepository fuelEntryRepository)
    {
        this.fuelEntryRepository = fuelEntryRepository;
    }

    public async Task<IEnumerable<FuelEntryDto>> GetFuelEntriesByVehicleIdAsync(int vehicleId)
    {
        var entries = await fuelEntryRepository.GetAllAsync();

        return entries
            .Where(x => x.VehicleId == vehicleId)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .Select(FuelEntryMapper.ToDto)
            .ToList();
    }

    public async Task AddFuelEntryAsync(FuelEntry fuelEntry)
    {
        fuelEntry.Consumption = CalculateConsumption(fuelEntry.Liters, fuelEntry.Distance);
        fuelEntry.CostPerKilometer = CalculateCostPerKilometer(fuelEntry.TotalPrice, fuelEntry.Distance);

        await fuelEntryRepository.AddAsync(fuelEntry);
    }

    public decimal CalculateConsumption(decimal liters, int distance)
    {
        if (distance <= 0)
            return 0;

        return liters / distance * 100;
    }

    public decimal CalculateCostPerKilometer(decimal totalPrice, int distance)
    {
        if (distance <= 0)
            return 0;

        return totalPrice / distance;
    }

    public async Task<decimal> GetTotalFuelLitersAsync(int vehicleId)
    {
        var entries = await fuelEntryRepository.GetAllAsync();

        return entries
            .Where(x => x.VehicleId == vehicleId)
            .Sum(x => x.Liters);
    }

    public async Task<decimal> GetTotalFuelCostAsync(int vehicleId)
    {
        var entries = await fuelEntryRepository.GetAllAsync();

        return entries
            .Where(x => x.VehicleId == vehicleId)
            .Sum(x => x.TotalPrice);
    }

    public async Task<decimal> GetAverageConsumptionAsync(int vehicleId)
    {
        var fuelEntries = await fuelEntryRepository.GetAllAsync();

        var consumptions = fuelEntries
            .Where(x => x.VehicleId == vehicleId && x.Consumption.HasValue && x.Consumption.Value > 0)
            .Select(x => x.Consumption!.Value)
            .ToList();

        if (!consumptions.Any())
            return 0;

        return consumptions.Average();
    }

    public async Task<decimal?> GetLastConsumptionAsync(int vehicleId)
    {
        var entries = await fuelEntryRepository.GetAllAsync();

        return entries
            .Where(x => x.VehicleId == vehicleId &&
                        x.Consumption.HasValue &&
                        x.Consumption.Value > 0)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .Select(x => x.Consumption)
            .FirstOrDefault();
    }
}