using AutoFy.Core.Enums;
using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.DTOs;
using AutoFy.Services.Services;
using Moq;

namespace AutoFy.Tests.Services;

public class VehicleServiceTests
{
    private readonly Mock<IVehicleRepository> vehicleRepositoryMock;
    private readonly VehicleService vehicleService;

    public VehicleServiceTests()
    {
        vehicleRepositoryMock = new Mock<IVehicleRepository>();
        vehicleService = new VehicleService(vehicleRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnVehicleDtos()
    {
        var vehicles = new List<Vehicle>
        {
            new()
            {
                Id = 1,
                Brand = "BMW",
                Model = "320d",
                Year = 2015,
                Mileage = 250000,
                FuelType = FuelType.Дизел,
                TransmissionType = TransmissionType.Автоматична
            }
        };

        vehicleRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(vehicles);

        var result = await vehicleService.GetAllAsync();

        var vehicle = result.Single();

        Assert.Equal(1, vehicle.Id);
        Assert.Equal("BMW", vehicle.Brand);
        Assert.Equal("320d", vehicle.Model);
        Assert.Equal(2015, vehicle.Year);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnVehicleDto_WhenVehicleExists()
    {
        var vehicle = new Vehicle
        {
            Id = 1,
            Brand = "Audi",
            Model = "A4",
            Year = 2018,
            FuelType = FuelType.Бензин,
            TransmissionType = TransmissionType.Ръчна
        };

        vehicleRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(vehicle);

        var result = await vehicleService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Audi", result!.Brand);
        Assert.Equal("A4", result.Model);
    }

    [Fact]
    public async Task AddAsync_ShouldMapDtoToEntityAndCallRepository()
    {
        var dto = new VehicleDto
        {
            Brand = "Mercedes",
            Model = "C400",
            Year = 2016,
            Mileage = 180000,
            FuelType = FuelType.Бензин,
            TransmissionType = TransmissionType.Автоматична
        };

        await vehicleService.AddAsync(dto);

        vehicleRepositoryMock.Verify(
            x => x.AddAsync(It.Is<Vehicle>(v =>
                v.Brand == "Mercedes" &&
                v.Model == "C400" &&
                v.Year == 2016 &&
                v.Mileage == 180000)),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateExistingVehicle()
    {
        var existingVehicle = new Vehicle
        {
            Id = 1,
            Brand = "Old",
            Model = "Old",
            Year = 2000
        };

        var dto = new VehicleDto
        {
            Id = 1,
            Brand = "BMW",
            Model = "530d",
            Year = 2019,
            Mileage = 120000,
            FuelType = FuelType.Дизел,
            TransmissionType = TransmissionType.Автоматична
        };

        vehicleRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(existingVehicle);

        await vehicleService.UpdateAsync(dto);

        Assert.Equal("BMW", existingVehicle.Brand);
        Assert.Equal("530d", existingVehicle.Model);
        Assert.Equal(2019, existingVehicle.Year);
        Assert.Equal(120000, existingVehicle.Mileage);

        vehicleRepositoryMock.Verify(
            x => x.UpdateAsync(existingVehicle),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldCallRepository_WhenVehicleExists()
    {
        var vehicle = new Vehicle
        {
            Id = 1,
            Brand = "BMW",
            Model = "X5"
        };

        vehicleRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(vehicle);

        await vehicleService.DeleteAsync(1);

        vehicleRepositoryMock.Verify(
            x => x.DeleteAsync(vehicle),
            Times.Once);
    }
}