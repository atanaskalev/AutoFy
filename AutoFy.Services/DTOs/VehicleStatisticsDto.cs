namespace AutoFy.Services.DTOs;

public class VehicleStatisticsDto
{
    public int VehicleId { get; set; }

    public string VehicleName { get; set; } = string.Empty;

    public decimal TotalCost { get; set; }

    public decimal AverageFuelConsumption { get; set; }

    public string TotalCostText => $"{TotalCost:F2} лв";

    public string AverageFuelConsumptionText =>
        AverageFuelConsumption > 0
            ? $"{AverageFuelConsumption:F2} л/100км"
            : "-";
}