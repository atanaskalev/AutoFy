using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddFuelEntryViewModel : BaseViewModel
{
    private string _vehicleName = "BMW 320d";
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
        set => SetProperty(ref _totalPrice, value);
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

    public AddFuelEntryViewModel()
    {
        Title = "Добави зареждане";

        SaveFuelCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("AutoFy", "Зареждането ще бъде записано по-късно.", "OK");
        });
    }

    private void CalculatePreview()
    {
        if (!decimal.TryParse(Distance, out var distance) || distance <= 0 ||
            !decimal.TryParse(Liters, out var liters) || liters <= 0)
        {
            CalculatedConsumption = "-";
            CalculatedCostPerKm = "-";
            return;
        }

        var consumption = liters / distance * 100;
        CalculatedConsumption = $"{consumption:F2} л/100 км";

        if (decimal.TryParse(PricePerLiter, out var pricePerLiter) && pricePerLiter > 0)
        {
            var total = liters * pricePerLiter;
            var costPerKm = total / distance;

            TotalPrice = $"{total:F2}";
            CalculatedCostPerKm = $"{costPerKm:F2} лв/км";
        }
        else
        {
            CalculatedCostPerKm = "-";
        }
    }
}