using AutoFy.Services.DTOs;

namespace AutoFy.Services.Interfaces;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllAsync();

    Task<VehicleDto?> GetByIdAsync(int id);

    Task AddAsync(VehicleDto vehicleDto);

    Task UpdateAsync(VehicleDto vehicleDto);

    Task DeleteAsync(int id);
}