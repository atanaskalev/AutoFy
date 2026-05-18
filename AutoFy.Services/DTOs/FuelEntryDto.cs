namespace AutoFy.Services.DTOs;

public class FuelEntryDto
{
    public int Id { get; set; }
    public int VehicleId { get; set; }

    public DateTime Date { get; set; }

    public int Odometer { get; set; }
    public int Distance { get; set; }

    public decimal Liters { get; set; }
    public decimal PricePerLiter { get; set; }
    public decimal TotalPrice { get; set; }

    public decimal? Consumption { get; set; }
    public decimal? CostPerKilometer { get; set; }

    public string? Notes { get; set; }

    public string DateText => Date.ToString("dd.MM.yyyy");
    public string OdometerText => $"{Odometer} км";
    public string TotalPriceText => $"{TotalPrice:F2} лв";
    public string LitersText => $"{Liters:F2} л";
    public string ConsumptionText => Consumption.HasValue ? $"{Consumption.Value:F2}" : "-";
    public string PricePerLiterText => $"{PricePerLiter:F2} лв";
    public bool HasNotes => !string.IsNullOrWhiteSpace(Notes);
}