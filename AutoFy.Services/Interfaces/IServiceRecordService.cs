using AutoFy.Core.Models;

namespace AutoFy.Services.Interfaces;

public interface IServiceRecordService
{
    Task<IEnumerable<ServiceRecord>> GetAllAsync();

    Task AddAsync(ServiceRecord serviceRecord);
}