using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class HistoryViewModel : BaseViewModel
{
    private readonly IHistoryService historyService;
    private readonly IVehicleService vehicleService;

    private VehicleDto? selectedVehicle;
    private string selectedHistoryType = "Всички";
    private DateTime selectedDate = DateTime.Today;
    private bool useDateFilter;

    public ObservableCollection<HistoryItemDto> HistoryItems { get; } = new();

    public ObservableCollection<VehicleDto> Vehicles { get; } = new();

    public List<string> HistoryTypes { get; } =
    [
        "Всички",
        "Гориво",
        "Сервиз"
    ];

    public VehicleDto? SelectedVehicle
    {
        get => selectedVehicle;
        set
        {
            if (SetProperty(ref selectedVehicle, value))
                _ = LoadHistoryAsync();
        }
    }

    public string SelectedHistoryType
    {
        get => selectedHistoryType;
        set
        {
            if (SetProperty(ref selectedHistoryType, value))
                _ = LoadHistoryAsync();
        }
    }

    public DateTime SelectedDate
    {
        get => selectedDate;
        set
        {
            if (SetProperty(ref selectedDate, value))
                _ = LoadHistoryAsync();
        }
    }

    public bool UseDateFilter
    {
        get => useDateFilter;
        set
        {
            if (SetProperty(ref useDateFilter, value))
                _ = LoadHistoryAsync();
        }
    }

    public ICommand LoadHistoryCommand { get; }
    public ICommand ClearFiltersCommand { get; }
    public ICommand InitializeCommand { get; }

    public HistoryViewModel(
        IHistoryService historyService,
        IVehicleService vehicleService)
    {
        this.historyService = historyService;
        this.vehicleService = vehicleService;

        Title = "История";

        LoadHistoryCommand = new Command(async () => await LoadHistoryAsync());
        ClearFiltersCommand = new Command(async () => await ClearFiltersAsync());
        InitializeCommand = new Command(async () => await InitializeAsync());
    }

    public async Task InitializeAsync()
    {
        await LoadVehiclesAsync();
        await LoadHistoryAsync();
    }

    private async Task LoadVehiclesAsync()
    {
        Vehicles.Clear();

        var vehicles = await vehicleService.GetAllAsync();

        foreach (var vehicle in vehicles)
            Vehicles.Add(vehicle);
    }

    private async Task LoadHistoryAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            HistoryItems.Clear();

            var items = await historyService.GetHistoryAsync(
                SelectedVehicle?.Id,
                UseDateFilter ? SelectedDate : null,
                SelectedHistoryType);

            foreach (var item in items)
                HistoryItems.Add(item);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task ClearFiltersAsync()
    {
        SelectedVehicle = null;
        SelectedHistoryType = "Всички";
        UseDateFilter = false;
        SelectedDate = DateTime.Today;

        await LoadHistoryAsync();
    }
}