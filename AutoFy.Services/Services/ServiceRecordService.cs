using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.Interfaces;

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
}