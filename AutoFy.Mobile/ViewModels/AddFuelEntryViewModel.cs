using AutoFy.Core.Models;
using AutoFy.Services.Interfaces;
using System.Globalization;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddFuelEntryViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IFuelService fuelService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;

    private string _vehicleName = string.Empty;
    private DateTime _fuelDate = DateTime.Today;

    private string _odometer = string.Empty;
    private string _distance = string.Empty;
    private string _liters = string.Empty;
    private string _pricePerLiter = string.Empty;
    private string _totalPrice = string.Empty;
    private string _notes = string.Empty;

    private string _calculatedConsumption = "-";
    private string _calculatedCostPerKm = "-";

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public DateTime FuelDate
    {
        get => _fuelDate;
        set => SetProperty(ref _fuelDate, value);
    }

    public string Odometer
    {
        get => _odometer;
        set => SetProperty(ref _odometer, value);
    }

    public string Distance
    {
        get => _distance;
        set
        {
            if (SetProperty(ref _distance, value))
                CalculatePreview();
        }
    }

    public string Liters
    {
        get => _liters;
        set
        {
            if (SetProperty(ref _liters, value))
                CalculatePreview();
        }
    }

    public string PricePerLiter
    {
        get => _pricePerLiter;
        set
        {
            if (SetProperty(ref _pricePerLiter, value))
                CalculatePreview();
        }
    }

    public string TotalPrice
    {
        get => _totalPrice;
        set
        {
            if (SetProperty(ref _totalPrice, value))
                CalculatePreview();
        }
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public string CalculatedConsumption
    {
        get => _calculatedConsumption;
        set => SetProperty(ref _calculatedConsumption, value);
    }

    public string CalculatedCostPerKm
    {
        get => _calculatedCostPerKm;
        set => SetProperty(ref _calculatedCostPerKm, value);
    }

    public ICommand SaveFuelCommand { get; }

    public AddFuelEntryViewModel(
        IFuelService fuelService,
        IVehicleService vehicleService)
    {
        this.fuelService = fuelService;
        this.vehicleService = vehicleService;

        Title = "Добави зареждане";

        SaveFuelCommand = new Command(async () => await SaveFuelAsync());
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("VehicleId", out var vehicleIdValue))
            return;

        if (!int.TryParse(vehicleIdValue?.ToString(), out vehicleId))
            return;

        var vehicle = await vehicleService.GetByIdAsync(vehicleId);

        if (vehicle != null)
            VehicleName = vehicle.DisplayName;
    }

    private async Task SaveFuelAsync()
    {
        if (vehicleId <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Не е избран автомобил.", "OK");
            return;
        }

        if (!int.TryParse(Odometer, out var parsedOdometer))
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Въведи валиден километраж.", "OK");
            return;
        }

        if (!int.TryParse(Distance, out var parsedDistance) || parsedDistance <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Въведи валидни изминати километри.", "OK");
            return;
        }

        if (!TryParseDecimal(Liters, out var parsedLiters) || parsedLiters <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Въведи валидни литри.", "OK");
            return;
        }

        if (!TryParseDecimal(PricePerLiter, out var parsedPricePerLiter) || parsedPricePerLiter <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Въведи валидна цена на литър.", "OK");
            return;
        }

        var totalPrice = parsedLiters * parsedPricePerLiter;

        var fuelEntry = new FuelEntry
        {
            VehicleId = vehicleId,
            Date = FuelDate,
            Odometer = parsedOdometer,
            Distance = parsedDistance,
            Liters = parsedLiters,
            PricePerLiter = parsedPricePerLiter,
            TotalPrice = totalPrice,
            Notes = Notes?.Trim()
        };

        await fuelService.AddFuelEntryAsync(fuelEntry);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            "Зареждането е записано успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }

    private void CalculatePreview()
    {
        if (!TryParseDecimal(Distance, out var distance) || distance <= 0 ||
            !TryParseDecimal(Liters, out var liters) || liters <= 0)
        {
            CalculatedConsumption = "-";
            CalculatedCostPerKm = "-";
            return;
        }

        var consumption = liters / distance * 100;
        CalculatedConsumption = $"{consumption:F2} л/100 км";

        if (TryParseDecimal(PricePerLiter, out var pricePerLiter) && pricePerLiter > 0)
        {
            var total = liters * pricePerLiter;
            var costPerKm = total / distance;

            TotalPrice = $"{total:F2}";
            CalculatedCostPerKm = $"{costPerKm:F2} лв/км";
        }
        else if (TryParseDecimal(TotalPrice, out var totalPrice) && totalPrice > 0)
        {
            var costPerKm = totalPrice / distance;
            CalculatedCostPerKm = $"{costPerKm:F2} лв/км";
        }
        else
        {
            CalculatedCostPerKm = "-";
        }
    }

    private static bool TryParseDecimal(string value, out decimal result)
    {
        value = value.Replace(',', '.');

        return decimal.TryParse(
            value,
            NumberStyles.Number,
            CultureInfo.InvariantCulture,
            out result);
    }
}