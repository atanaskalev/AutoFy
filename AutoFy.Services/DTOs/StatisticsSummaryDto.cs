namespace AutoFy.Services.DTOs;

public class StatisticsSummaryDto
{
    public decimal TotalCost { get; set; }

    public List<VehicleStatisticsDto> Vehicles { get; set; } = new();

    public string TotalCostText => $"{TotalCost:F2} €";
}