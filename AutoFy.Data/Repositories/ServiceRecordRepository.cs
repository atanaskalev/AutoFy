using AutoFy.Core.Models;
using AutoFy.Data.Context;
using AutoFy.Data.Repositories.Base;
using AutoFy.Data.Repositories.Interfaces;

namespace AutoFy.Data.Repositories.Implementations;

public class ServiceRecordRepository
    : Repository<ServiceRecord>, IServiceRecordRepository
{
    public ServiceRecordRepository(AutoFyDbContext context)
        : base(context)
    {
    }
}