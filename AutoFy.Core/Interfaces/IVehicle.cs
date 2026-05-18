using AutoFy.Core.Enums;
using Microsoft.VisualBasic.FileIO;

namespace AutoFy.Core.Interfaces;

public interface IVehicle : IEntity
{
    string Brand { get; set; }

    string Model { get; set; }

    int Year { get; set; }

    string LicensePlate { get; set; }

    string Vin { get; set; }

    int Mileage { get; set; }

    FuelType FuelType { get; set; }

    TransmissionType TransmissionType { get; set; }
}