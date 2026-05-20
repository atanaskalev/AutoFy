namespace AutoFy.Services.DTOs;

public class HistoryItemDto
{
    public int VehicleId { get; set; }

    public string VehicleName { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Subtitle { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string DateText => Date.ToString("dd.MM.yyyy");

    public string AmountText => $"{Amount:F2} лв";
}