using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;

namespace AutoFy.Services.Services;

public class HistoryService : IHistoryService
{
    private const string AllTypes = "Всички";
    private const string FuelType = "Гориво";
    private const string ServiceType = "Сервиз";

    private readonly IVehicleRepository vehicleRepository;
    private readonly IFuelEntryRepository fuelEntryRepository;
    private readonly IServiceRecordRepository serviceRecordRepository;

    public HistoryService(
        IVehicleRepository vehicleRepository,
        IFuelEntryRepository fuelEntryRepository,
        IServiceRecordRepository serviceRecordRepository)
    {
        this.vehicleRepository = vehicleRepository;
        this.fuelEntryRepository = fuelEntryRepository;
        this.serviceRecordRepository = serviceRecordRepository;
    }

    public async Task<IEnumerable<HistoryItemDto>> GetHistoryAsync(
        int? vehicleId,
        DateTime? date,
        string historyType)
    {
        var vehicles = await vehicleRepository.GetAllAsync();
        var vehicleNames = vehicles.ToDictionary(
            x => x.Id,
            x => $"{x.Brand} {x.Model}");

        var result = new List<HistoryItemDto>();

        if (historyType == AllTypes || historyType == FuelType)
        {
            var fuelEntries = await fuelEntryRepository.GetAllAsync();

            var fuelHistory = fuelEntries.Select(x => new HistoryItemDto
            {
                VehicleId = x.VehicleId,
                VehicleName = vehicleNames.TryGetValue(x.VehicleId, out var name) ? name : "Неизвестен автомобил",
                Date = x.Date,
                Type = FuelType,
                Title = "Зареждане с гориво",
                Subtitle = $"{x.Liters:F2} л • {x.Distance} км • {x.Consumption:F2} л/100км",
                Amount = x.TotalPrice
            });

            result.AddRange(fuelHistory);
        }

        if (historyType == AllTypes || historyType == ServiceType)
        {
            var serviceRecords = await serviceRecordRepository.GetAllAsync();

            var serviceHistory = serviceRecords.Select(x => new HistoryItemDto
            {
                VehicleId = x.VehicleId,
                VehicleName = vehicleNames.TryGetValue(x.VehicleId, out var name) ? name : "Неизвестен автомобил",
                Date = x.Date,
                Type = ServiceType,
                Title = GetServiceTypeText(x.ServiceType),
                Subtitle = string.IsNullOrWhiteSpace(x.Description)
                    ? $"{x.Odometer} км"
                    : $"{x.Odometer} км • {x.Description}",
                Amount = x.Price
            });

            result.AddRange(serviceHistory);
        }

        if (vehicleId.HasValue && vehicleId.Value > 0)
            result = result.Where(x => x.VehicleId == vehicleId.Value).ToList();

        if (date.HasValue)
            result = result.Where(x => x.Date.Date == date.Value.Date).ToList();

        return result
            .OrderByDescending(x => x.Date)
            .ToList();
    }

    private static string GetServiceTypeText(Core.Enums.ServiceType serviceType)
    {
        return serviceType switch
        {
            Core.Enums.ServiceType.Maintenance => "Обслужване",
            Core.Enums.ServiceType.Repair => "Ремонт",
            Core.Enums.ServiceType.TireChange => "Смяна на гуми",
            Core.Enums.ServiceType.Diagnostics => "Диагностика",
            Core.Enums.ServiceType.TechnicalInspection => "Технически преглед",
            Core.Enums.ServiceType.Other => "Друго",
            _ => "Друго"
        };
    }
}