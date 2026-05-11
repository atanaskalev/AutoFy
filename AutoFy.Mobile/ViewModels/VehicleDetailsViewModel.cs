using AutoFy.Mobile.Views;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class VehicleDetailsViewModel : BaseViewModel
{
    private string _vehicleName = "BMW 320d";
    private string _vehicleShortInfo = "2014 • Дизел • 245 000 км";
    private string _licensePlate = "CA 1234 AB";

    private string _averageFuelConsumption = "6.2 л/100км";
    private string _costPerKilometer = "0.18 лв";

    private string _technicalInspectionDateText = "12.08.2026";
    private string _insuranceDateText = "05.05.2026";
    private string _vignetteDateText = "Активна";
    private string _fireExtinguisherDateText = "20.09.2026";

    private string _lastFuelEntryText = "Последно: 45 л";
    private string _totalFuelLiters = "820 л";
    private string _totalFuelCost = "2 160 лв";

    private string _lastServiceRecordText = "Последна дейност: Смяна на масло";

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

    public string CostPerKilometer
    {
        get => _costPerKilometer;
        set => SetProperty(ref _costPerKilometer, value);
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

    public VehicleDetailsViewModel()
    {
        Title = "Детайли за автомобил";

        OpenAddFuelCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(AddFuelEntryView)));

        OpenFuelHistoryCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(FuelHistoryView)));

        OpenAddReminderCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(AddReminderView)));

        OpenAddServiceRecordCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(AddServiceRecordView)));
    }
}