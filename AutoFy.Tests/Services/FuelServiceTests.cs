using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.Services;
using Moq;

namespace AutoFy.Tests.Services;

public class FuelServiceTests
{
    private readonly Mock<IFuelEntryRepository> fuelEntryRepositoryMock;
    private readonly FuelService fuelService;

    public FuelServiceTests()
    {
        fuelEntryRepositoryMock = new Mock<IFuelEntryRepository>();
        fuelService = new FuelService(fuelEntryRepositoryMock.Object);
    }

    [Fact]
    public void CalculateConsumption_ShouldReturnCorrectConsumption()
    {
        var result = fuelService.CalculateConsumption(50, 800);

        Assert.Equal(6.25m, result);
    }

    [Fact]
    public void CalculateConsumption_ShouldReturnZero_WhenDistanceIsZero()
    {
        var result = fuelService.CalculateConsumption(50, 0);

        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateCostPerKilometer_ShouldReturnCorrectCost()
    {
        var result = fuelService.CalculateCostPerKilometer(160, 800);

        Assert.Equal(0.2m, result);
    }

    [Fact]
    public void CalculateCostPerKilometer_ShouldReturnZero_WhenDistanceIsZero()
    {
        var result = fuelService.CalculateCostPerKilometer(160, 0);

        Assert.Equal(0, result);
    }

    [Fact]
    public async Task AddFuelEntryAsync_ShouldCalculateValuesAndSaveEntry()
    {
        var fuelEntry = new FuelEntry
        {
            VehicleId = 1,
            Liters = 50,
            Distance = 800,
            TotalPrice = 160,
            PricePerLiter = 3.2m
        };

        await fuelService.AddFuelEntryAsync(fuelEntry);

        Assert.Equal(6.25m, fuelEntry.Consumption);
        Assert.Equal(0.2m, fuelEntry.CostPerKilometer);

        fuelEntryRepositoryMock.Verify(
            x => x.AddAsync(fuelEntry),
            Times.Once);
    }
}