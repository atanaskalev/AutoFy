using AutoFy.Core.Models;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Globalization;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddFuelEntryViewModel : BaseViewModel, IQueryAttributable
{
    #region Fields

    private readonly IFuelService fuelService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;
    private int? fuelEntryId;

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

    #endregion

    #region Init

    public AddFuelEntryViewModel(IFuelService fuelService, IVehicleService vehicleService)
    {
        this.fuelService = fuelService;
        this.vehicleService = vehicleService;

        Title = "Добави зареждане";

        SaveFuelCommand = new Command(async () => await SaveFuelAsync());
        DeleteFuelCommand = new Command(async () => await DeleteFuelAsync());
    }

    #endregion

    #region Properties

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

    public string SaveButtonText =>
        fuelEntryId.HasValue ? "Запази промените" : "Запази зареждането";

    #endregion

    #region Commands

    public ICommand SaveFuelCommand { get; }
    public ICommand DeleteFuelCommand { get; }

    #endregion

    #region Methods

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("FuelEntryId", out var fuelEntryIdValue) &&
            int.TryParse(fuelEntryIdValue?.ToString(), out var parsedFuelEntryId))
        {
            fuelEntryId = parsedFuelEntryId;
            Title = "Редактирай зареждане";
            OnPropertyChanged(nameof(SaveButtonText));

            await LoadFuelEntryAsync(parsedFuelEntryId);
            return;
        }

        if (query.TryGetValue("VehicleId", out var vehicleIdValue) &&
            int.TryParse(vehicleIdValue?.ToString(), out vehicleId))
        {
            var vehicle = await vehicleService.GetByIdAsync(vehicleId);

            if (vehicle != null)
                VehicleName = vehicle.DisplayName;
        }
    }

    private async Task LoadFuelEntryAsync(int id)
    {
        var fuelEntry = await fuelService.GetByIdAsync(id);

        if (fuelEntry == null)
            return;

        vehicleId = fuelEntry.VehicleId;

        var vehicle = await vehicleService.GetByIdAsync(vehicleId);

        if (vehicle != null)
            VehicleName = vehicle.DisplayName;

        FuelDate = fuelEntry.Date;
        Odometer = fuelEntry.Odometer.ToString();
        Distance = fuelEntry.Distance.ToString();
        Liters = fuelEntry.Liters.ToString("F2");
        PricePerLiter = fuelEntry.PricePerLiter.ToString("F2");
        TotalPrice = fuelEntry.TotalPrice.ToString("F2");
        Notes = fuelEntry.Notes ?? string.Empty;

        CalculatePreview();
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

        if (fuelEntryId.HasValue)
        {
            var fuelEntryDto = new FuelEntryDto
            {
                Id = fuelEntryId.Value,
                VehicleId = vehicleId,
                Date = FuelDate,
                Odometer = parsedOdometer,
                Distance = parsedDistance,
                Liters = parsedLiters,
                PricePerLiter = parsedPricePerLiter,
                TotalPrice = totalPrice,
                Notes = Notes?.Trim()
            };

            await fuelService.UpdateFuelEntryAsync(fuelEntryDto);
        }
        else
        {
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
        }

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            fuelEntryId.HasValue
                ? "Зареждането е редактирано успешно."
                : "Зареждането е записано успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }

    private async Task DeleteFuelAsync()
    {
        if (!fuelEntryId.HasValue)
            return;

        var confirmed = await Shell.Current.DisplayAlertAsync(
            "Изтриване",
            "Сигурен ли си, че искаш да изтриеш това зареждане?",
            "Да",
            "Не");

        if (!confirmed)
            return;

        await fuelService.DeleteFuelEntryAsync(fuelEntryId.Value);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            "Зареждането е изтрито успешно.",
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
            CalculatedCostPerKm = $"{costPerKm:F2} €/км";
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
    #endregion

}