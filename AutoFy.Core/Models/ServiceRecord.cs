using AutoFy.Core.Base;
using AutoFy.Core.Enums;
using AutoFy.Core.Interfaces;

namespace AutoFy.Core.Models;

public class ServiceRecord : BaseEntity, IServiceRecord
{
    public int VehicleId { get; set; }

    public Vehicle? Vehicle { get; set; }

    public ServiceType ServiceType { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    public int Odometer { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceLocation { get; set; }
}