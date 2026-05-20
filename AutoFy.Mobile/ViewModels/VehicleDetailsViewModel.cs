using AutoFy.Mobile.Views;
using AutoFy.Services.Interfaces;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class VehicleDetailsViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IVehicleService vehicleService;
    private readonly IFuelService fuelService;
    private readonly IServiceRecordService serviceRecordService;

    private int vehicleId;
    private string? imagePath;
    private string _vehicleName = string.Empty;
    private string _vehicleShortInfo = string.Empty;
    private string _licensePlate = string.Empty;

    private string _averageFuelConsumption = "-";
    private string _totalVehicleCost = "-";
    private string _lastConsumption = "-";

    private string _insurancePriceText = "-";
    private string _vignettePriceText = "-";
    private string _technicalInspectionPriceText = "-";
    private string _fireExtinguisherPriceText = "-";

    private string _technicalInspectionDateText = "-";
    private string _insuranceDateText = "-";
    private string _vignetteDateText = "-";
    private string _fireExtinguisherDateText = "-";

    private string _lastFuelEntryText = "Няма зареждания";
    private string _totalFuelLiters = "0 л";
    private string _totalFuelCost = "0 лв";

    private string _lastServiceRecordText = "Няма добавени сервизни дейности";

    public string? ImagePath
    {
        get => imagePath;
        set => SetProperty(ref imagePath, value);
    }

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public string VehicleShortInfo
    {
        get => _vehicleShortInfo;
        set => SetProperty(ref _vehicleShortInfo, value);
    }

    public string LicensePlate
    {
        get => _licensePlate;
        set => SetProperty(ref _licensePlate, value);
    }

    public string AverageFuelConsumption
    {
        get => _averageFuelConsumption;
        set => SetProperty(ref _averageFuelConsumption, value);
    }

    public string TotalVehicleCost
    {
        get => _totalVehicleCost;
        set => SetProperty(ref _totalVehicleCost, value);
    }

    public string LastConsumption
    {
        get => _lastConsumption;
        set => SetProperty(ref _lastConsumption, value);
    }

    public string InsurancePriceText
    {
        get => _insurancePriceText;
        set => SetProperty(ref _insurancePriceText, value);
    }

    public string VignettePriceText
    {
        get => _vignettePriceText;
        set => SetProperty(ref _vignettePriceText, value);
    }

    public string TechnicalInspectionPriceText
    {
        get => _technicalInspectionPriceText;
        set => SetProperty(ref _technicalInspectionPriceText, value);
    }

    public string FireExtinguisherPriceText
    {
        get => _fireExtinguisherPriceText;
        set => SetProperty(ref _fireExtinguisherPriceText, value);
    }

    public string TechnicalInspectionDateText
    {
        get => _technicalInspectionDateText;
        set => SetProperty(ref _technicalInspectionDateText, value);
    }

    public string InsuranceDateText
    {
        get => _insuranceDateText;
        set => SetProperty(ref _insuranceDateText, value);
    }

    public string VignetteDateText
    {
        get => _vignetteDateText;
        set => SetProperty(ref _vignetteDateText, value);
    }

    public string FireExtinguisherDateText
    {
        get => _fireExtinguisherDateText;
        set => SetProperty(ref _fireExtinguisherDateText, value);
    }

    public string LastFuelEntryText
    {
        get => _lastFuelEntryText;
        set => SetProperty(ref _lastFuelEntryText, value);
    }

    public string TotalFuelLiters
    {
        get => _totalFuelLiters;
        set => SetProperty(ref _totalFuelLiters, value);
    }

    public string TotalFuelCost
    {
        get => _totalFuelCost;
        set => SetProperty(ref _totalFuelCost, value);
    }

    public string LastServiceRecordText
    {
        get => _lastServiceRecordText;
        set => SetProperty(ref _lastServiceRecordText, value);
    }

    public ICommand OpenAddFuelCommand { get; }
    public ICommand OpenFuelHistoryCommand { get; }
    public ICommand OpenAddReminderCommand { get; }
    public ICommand OpenAddServiceRecordCommand { get; }
    public ICommand DeleteVehicleCommand { get; }
    public ICommand EditVehicleCommand { get; }

    public ICommand OpenServiceHistoryCommand { get; }

    public VehicleDetailsViewModel(IVehicleService vehicleService, IFuelService fuelService, IServiceRecordService serviceRecordService)
    {
        this.vehicleService = vehicleService;
        this.fuelService = fuelService;
        this.serviceRecordService = serviceRecordService;

        Title = "Детайли за автомобил";

        OpenAddFuelCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(AddFuelEntryView)}?VehicleId={vehicleId}"));

        OpenFuelHistoryCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(FuelHistoryView)}?VehicleId={vehicleId}"));

        OpenAddReminderCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(AddReminderView)));

        OpenAddServiceRecordCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(AddServiceRecordView)}?VehicleId={vehicleId}"));

        DeleteVehicleCommand = new Command(async () => await DeleteVehicleAsync());

        EditVehicleCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(AddVehicleView)}?VehicleId={vehicleId}"));

        OpenServiceHistoryCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(ServiceHistoryView)}?VehicleId={vehicleId}"));

        this.serviceRecordService = serviceRecordService;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("VehicleId", out var vehicleIdValue))
            return;

        if (!int.TryParse(vehicleIdValue?.ToString(), out vehicleId))
            return;

        await LoadVehicleAsync(vehicleId);
    }

    private async Task LoadVehicleAsync(int id)
    {
        var vehicle = await vehicleService.GetByIdAsync(id);

        if (vehicle == null)
            return;

        ImagePath = vehicle.ImagePath;
        VehicleName = vehicle.DisplayName;
        VehicleShortInfo = $"{vehicle.Year} • {vehicle.FuelType} • {vehicle.Mileage} км";
        LicensePlate = string.IsNullOrWhiteSpace(vehicle.LicensePlate)
            ? "Няма регистрационен номер"
            : vehicle.LicensePlate;

        TechnicalInspectionDateText = FormatDate(vehicle.TechnicalInspectionDate);
        TechnicalInspectionPriceText = $"{vehicle.TechnicalInspectionPrice:F2} €";
        InsuranceDateText = FormatDate(vehicle.InsuranceDate);
        InsurancePriceText = $"{vehicle.InsurancePrice:F2} €";
        VignetteDateText = FormatDate(vehicle.VignetteDate);
        VignettePriceText = $"{vehicle.VignettePrice:F2} €";
        FireExtinguisherDateText = FormatDate(vehicle.FireExtinguisherDate);
        FireExtinguisherPriceText = $"{vehicle.FireExtinguisherPrice:F2} €";

        var averageConsumption = await fuelService.GetAverageConsumptionAsync(id);
        var totalFuelLiters = await fuelService.GetTotalFuelLitersAsync(id);
        var totalFuelCost = await fuelService.GetTotalFuelCostAsync(id);
        var lastConsumption = await fuelService.GetLastConsumptionAsync(id);
        var lastServiceRecord = await serviceRecordService.GetLastServiceRecordAsync(id);
        var totalServiceCost = await serviceRecordService.GetTotalServiceCostAsync(id);
        var fixedCosts = vehicle.InsurancePrice +
                         vehicle.VignettePrice +
                         vehicle.TechnicalInspectionPrice +
                         vehicle.FireExtinguisherPrice;

        AverageFuelConsumption = averageConsumption > 0
            ? $"{averageConsumption:F2} л/100км"
            : "-";

        TotalFuelLiters = $"{totalFuelLiters:F2} л";

        TotalFuelCost = $"{totalFuelCost:F2} €";

        LastConsumption = lastConsumption.HasValue
            ? $"{lastConsumption.Value:F2} л/100км"
            : "-";

        LastServiceRecordText = lastServiceRecord == null
            ? "Няма добавени сервизни дейности"
            : $"Последна дейност: {lastServiceRecord.ServiceType} - {lastServiceRecord.Price:F2} €";

        TotalVehicleCost = $"{totalFuelCost + totalServiceCost + fixedCosts:F2} лв";
    }

    private async Task DeleteVehicleAsync()
    {
        if (vehicleId <= 0)
            return;

        bool confirmed = await Shell.Current.DisplayAlertAsync(
            "Изтриване",
            $"Сигурен ли си, че искаш да изтриеш {VehicleName}?",
            "Да",
            "Не");

        if (!confirmed)
            return;

        await vehicleService.DeleteAsync(vehicleId);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            "Автомобилът е изтрит успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }

    private static string FormatDate(DateTime? date)
    {
        return date.HasValue
            ? date.Value.ToString("dd.MM.yyyy")
            : "-";
    }
}