using AutoFy.Core.Enums;
using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.Services;
using Moq;

namespace AutoFy.Tests.Services;

public class StatisticsServiceTests
{
    private readonly Mock<IVehicleRepository> vehicleRepositoryMock;
    private readonly Mock<IFuelEntryRepository> fuelEntryRepositoryMock;
    private readonly Mock<IServiceRecordRepository> serviceRecordRepositoryMock;
    private readonly StatisticsService statisticsService;

    public StatisticsServiceTests()
    {
        vehicleRepositoryMock = new Mock<IVehicleRepository>();
        fuelEntryRepositoryMock = new Mock<IFuelEntryRepository>();
        serviceRecordRepositoryMock = new Mock<IServiceRecordRepository>();

        statisticsService = new StatisticsService(
            vehicleRepositoryMock.Object,
            fuelEntryRepositoryMock.Object,
            serviceRecordRepositoryMock.Object);
    }

    [Fact]
    public async Task GetStatisticsAsync_ShouldCalculateVehicleTotalCost()
    {
        var vehicles = new List<Vehicle>
        {
            new()
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                InsurancePrice = 200,
                VignettePrice = 100,
                TechnicalInspectionPrice = 50,
                FireExtinguisherPrice = 20
            }
        };

        var fuelEntries = new List<FuelEntry>
        {
            new()
            {
                VehicleId = 1,
                TotalPrice = 150,
                Consumption = 6.5m
            }
        };

        var serviceRecords = new List<ServiceRecord>
        {
            new()
            {
                VehicleId = 1,
                Price = 300,
                ServiceType = ServiceType.Обслужване
            }
        };

        vehicleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(vehicles);

        fuelEntryRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(fuelEntries);

        serviceRecordRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(serviceRecords);

        var result = await statisticsService.GetStatisticsAsync();

        var vehicleStatistics = result.Vehicles.Single();

        Assert.Equal(820, vehicleStatistics.TotalCost);
        Assert.Equal(820, result.TotalCost);
    }

    [Fact]
    public async Task GetStatisticsAsync_ShouldCalculateAverageFuelConsumption()
    {
        var vehicles = new List<Vehicle>
        {
            new()
            {
                Id = 1,
                Brand = "Audi",
                Model = "A4"
            }
        };

        var fuelEntries = new List<FuelEntry>
        {
            new() { VehicleId = 1, Consumption = 6 },
            new() { VehicleId = 1, Consumption = 8 }
        };

        vehicleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(vehicles);

        fuelEntryRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(fuelEntries);

        serviceRecordRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<ServiceRecord>());

        var result = await statisticsService.GetStatisticsAsync();

        var vehicleStatistics = result.Vehicles.Single();

        Assert.Equal(7, vehicleStatistics.AverageFuelConsumption);
    }

    [Fact]
    public async Task GetStatisticsAsync_ShouldReturnZero_WhenVehicleHasNoCosts()
    {
        var vehicles = new List<Vehicle>
        {
            new()
            {
                Id = 1,
                Brand = "Mercedes",
                Model = "C400"
            }
        };

        vehicleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(vehicles);

        fuelEntryRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<FuelEntry>());

        serviceRecordRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(new List<ServiceRecord>());

        var result = await statisticsService.GetStatisticsAsync();

        var vehicleStatistics = result.Vehicles.Single();

        Assert.Equal(0, vehicleStatistics.TotalCost);
        Assert.Equal(0, vehicleStatistics.AverageFuelConsumption);
    }
}