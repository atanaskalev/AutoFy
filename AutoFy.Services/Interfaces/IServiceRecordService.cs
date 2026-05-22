using AutoFy.Core.Models;
using AutoFy.Services.DTOs;

namespace AutoFy.Services.Interfaces;

public interface IServiceRecordService
{
    Task<IEnumerable<ServiceRecord>> GetAllAsync();

    Task AddAsync(ServiceRecord serviceRecord);

    Task<decimal> GetTotalServiceCostAsync(int vehicleId);

    Task<ServiceRecord?> GetLastServiceRecordAsync(int vehicleId);

    Task<IEnumerable<ServiceRecordDto>> GetServiceRecordsByVehicleIdAsync(int vehicleId);

    Task<ServiceRecordDto?> GetByIdAsync(int id);

    Task UpdateAsync(ServiceRecordDto serviceRecordDto);

    Task DeleteAsync(int id);
}