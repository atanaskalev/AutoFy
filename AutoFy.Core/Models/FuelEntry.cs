using AutoFy.Core.Base;
using AutoFy.Core.Interfaces;

namespace AutoFy.Core.Models;

public class FuelEntry : BaseEntity, IFuelEntry
{
    public int VehicleId { get; set; }

    public Vehicle? Vehicle { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    public int Odometer { get; set; }

    public decimal Liters { get; set; }

    public decimal PricePerLiter { get; set; }

    public decimal TotalPrice { get; set; }

    public int Distance { get; set; }

    public decimal? Consumption { get; set; }

    public decimal? CostPerKilometer { get; set; }

    public string? Notes { get; set; }
}