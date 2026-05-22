using AutoFy.Core.Models;
using AutoFy.Services.DTOs;

namespace AutoFy.Services.Mappers;

public static class ServiceRecordMapper
{
    public static ServiceRecordDto ToDto(ServiceRecord serviceRecord)
    {
        return new ServiceRecordDto
        {
            Id = serviceRecord.Id,
            VehicleId = serviceRecord.VehicleId,
            ServiceType = serviceRecord.ServiceType,
            Date = serviceRecord.Date,
            Odometer = serviceRecord.Odometer,
            Price = serviceRecord.Price,
            Description = serviceRecord.Description,
            ServiceName = serviceRecord.ServiceName,
            ServiceLocation = serviceRecord.ServiceLocation
        };
    }

    public static void UpdateEntity(ServiceRecord entity, ServiceRecordDto dto)
    {
        entity.ServiceType = dto.ServiceType;
        entity.Date = dto.Date;
        entity.Odometer = dto.Odometer;
        entity.Price = dto.Price;
        entity.Description = dto.Description;
        entity.ServiceName = dto.ServiceName;
        entity.ServiceLocation = dto.ServiceLocation;
        entity.UpdatedAt = DateTime.Now;
    }
}