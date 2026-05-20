using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using AutoFy.Services.Mappers;

namespace AutoFy.Services.Services;

public class ServiceRecordService : IServiceRecordService
{
    private readonly IServiceRecordRepository serviceRecordRepository;

    public ServiceRecordService(IServiceRecordRepository serviceRecordRepository)
    {
        this.serviceRecordRepository = serviceRecordRepository;
    }

    public async Task<IEnumerable<ServiceRecord>> GetAllAsync()
    {
        return await serviceRecordRepository.GetAllAsync();
    }

    public async Task AddAsync(ServiceRecord serviceRecord)
    {
        await serviceRecordRepository.AddAsync(serviceRecord);
    }

    public async Task<decimal> GetTotalServiceCostAsync(int vehicleId)
    {
        var records = await serviceRecordRepository.GetAllAsync();

        return records
            .Where(x => x.VehicleId == vehicleId)
            .Sum(x => x.Price);
    }

    public async Task<ServiceRecord?> GetLastServiceRecordAsync(int vehicleId)
    {
        var records = await serviceRecordRepository.GetAllAsync();

        return records
            .Where(x => x.VehicleId == vehicleId)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .FirstOrDefault();
    }

    public async Task<IEnumerable<ServiceRecordDto>> GetServiceRecordsByVehicleIdAsync(int vehicleId)
    {
        var records = await serviceRecordRepository.GetAllAsync();

        return records
            .Where(x => x.VehicleId == vehicleId)
            .OrderByDescending(x => x.Date)
            .ThenByDescending(x => x.Id)
            .Select(ServiceRecordMapper.ToDto)
            .ToList();
    }
}