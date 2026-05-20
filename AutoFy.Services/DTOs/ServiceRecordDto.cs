using AutoFy.Core.Enums;

namespace AutoFy.Services.DTOs;

public class ServiceRecordDto
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public ServiceType ServiceType { get; set; }

    public DateTime Date { get; set; }

    public int Odometer { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? ServiceName { get; set; }

    public string? ServiceLocation { get; set; }

    public string DateText => Date.ToString("dd.MM.yyyy");

    public string ServiceTypeText => ServiceType switch
    {
        ServiceType.Maintenance => "Обслужване",
        ServiceType.Repair => "Ремонт",
        ServiceType.TireChange => "Смяна на гуми",
        ServiceType.Diagnostics => "Диагностика",
        ServiceType.TechnicalInspection => "Технически преглед",
        ServiceType.Other => "Друго",
        _ => "Друго"
    };

    public string OdometerText => $"{Odometer} км";

    public string PriceText => $"{Price:F2} лв";

    public bool HasDescription => !string.IsNullOrWhiteSpace(Description);

    public bool HasServiceName => !string.IsNullOrWhiteSpace(ServiceName);

    public bool HasServiceLocation => !string.IsNullOrWhiteSpace(ServiceLocation);
}