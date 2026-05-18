using AutoFy.Core.Enums;

namespace AutoFy.Core.Interfaces;

public interface IServiceRecord : IEntity
{
    int VehicleId { get; set; }

    ServiceType ServiceType { get; set; }

    DateTime Date { get; set; }

    int Odometer { get; set; }

    decimal Price { get; set; }
}