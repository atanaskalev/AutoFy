using AutoFy.Services.DTOs;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.Interfaces;
using AutoFy.Services.Mappers;

namespace AutoFy.Services.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        this.vehicleRepository = vehicleRepository;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllAsync()
    {
        var vehicles = await vehicleRepository.GetAllAsync();

        return vehicles
            .Select(VehicleMapper.ToDto)
            .ToList();
    }

    public async Task<VehicleDto?> GetByIdAsync(int id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);

        if (vehicle == null)
            return null;

        return VehicleMapper.ToDto(vehicle);
    }

    public async Task AddAsync(VehicleDto vehicleDto)
    {
        var vehicle = VehicleMapper.ToEntity(vehicleDto);

        await vehicleRepository.AddAsync(vehicle);
    }

    public async Task UpdateAsync(VehicleDto vehicleDto)
{
    var existingVehicle = await vehicleRepository.GetByIdAsync(vehicleDto.Id);

    if (existingVehicle == null)
        return;

    VehicleMapper.UpdateEntity(existingVehicle, vehicleDto);

    await vehicleRepository.UpdateAsync(existingVehicle);
}

    public async Task DeleteAsync(int id)
    {
        var vehicle = await vehicleRepository.GetByIdAsync(id);

        if (vehicle == null)
            return;

        await vehicleRepository.DeleteAsync(vehicle);
    }
}