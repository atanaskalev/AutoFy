using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;

namespace AutoFy.Services.Services;

public class StatisticsService : IStatisticsService
{
    #region Fields

    private readonly IVehicleRepository vehicleRepository;
    private readonly IFuelEntryRepository fuelEntryRepository;
    private readonly IServiceRecordRepository serviceRecordRepository;

    #endregion

    #region Init

    public StatisticsService(IVehicleRepository vehicleRepository, IFuelEntryRepository fuelEntryRepository, IServiceRecordRepository serviceRecordRepository)
    {
        this.vehicleRepository = vehicleRepository;
        this.fuelEntryRepository = fuelEntryRepository;
        this.serviceRecordRepository = serviceRecordRepository;
    }

    #endregion

    #region Methods

    public async Task<StatisticsSummaryDto> GetStatisticsAsync()
    {
        var vehicles = (await vehicleRepository.GetAllAsync()).ToList();
        var fuelEntries = (await fuelEntryRepository.GetAllAsync()).ToList();
        var serviceRecords = (await serviceRecordRepository.GetAllAsync()).ToList();

        var result = new StatisticsSummaryDto();

        foreach (var vehicle in vehicles)
        {
            var vehicleFuelEntries = fuelEntries
                .Where(x => x.VehicleId == vehicle.Id)
                .ToList();

            var vehicleServiceRecords = serviceRecords
                .Where(x => x.VehicleId == vehicle.Id)
                .ToList();

            var fuelCost = vehicleFuelEntries.Sum(x => x.TotalPrice);
            var serviceCost = vehicleServiceRecords.Sum(x => x.Price);

            var fixedCosts =
                vehicle.InsurancePrice +
                vehicle.VignettePrice +
                vehicle.TechnicalInspectionPrice +
                vehicle.FireExtinguisherPrice;

            var totalCost = fuelCost + serviceCost + fixedCosts;

            var averageFuelConsumption = vehicleFuelEntries
                .Where(x => x.Consumption.HasValue && x.Consumption.Value > 0)
                .Select(x => x.Consumption!.Value)
                .DefaultIfEmpty(0)
                .Average();

            result.Vehicles.Add(new VehicleStatisticsDto
            {
                VehicleId = vehicle.Id,
                VehicleName = $"{vehicle.Brand} {vehicle.Model}",
                TotalCost = totalCost,
                AverageFuelConsumption = averageFuelConsumption
            });
        }

        result.TotalCost = result.Vehicles.Sum(x => x.TotalCost);

        return result;
    }

    #endregion
}