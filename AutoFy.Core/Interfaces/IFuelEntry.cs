namespace AutoFy.Core.Interfaces;

public interface IFuelEntry : IEntity
{
    int VehicleId { get; set; }

    DateTime Date { get; set; }

    int Odometer { get; set; }

    decimal Liters { get; set; }

    decimal PricePerLiter { get; set; }

    decimal TotalPrice { get; set; }

    int Distance { get; set; }
}