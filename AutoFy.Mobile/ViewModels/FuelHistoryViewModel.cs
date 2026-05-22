using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class FuelHistoryViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IFuelService fuelService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;

    private string _vehicleName = string.Empty;

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public ObservableCollection<FuelEntryDto> FuelEntries { get; } = new();

    public ICommand OpenEditFuelEntryCommand { get; }

    public FuelHistoryViewModel(
        IFuelService fuelService,
        IVehicleService vehicleService)
    {
        this.fuelService = fuelService;
        this.vehicleService = vehicleService;

        OpenEditFuelEntryCommand = new Command<FuelEntryDto>(async fuelEntry =>
        {
            if (fuelEntry == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(Views.AddFuelEntryView)}?FuelEntryId={fuelEntry.Id}");
        });

        Title = "История на зареждания";
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("VehicleId", out var vehicleIdValue))
            return;

        if (!int.TryParse(vehicleIdValue?.ToString(), out vehicleId))
            return;

        await LoadDataAsync();
    }

    private async Task LoadDataAsync()
    {
        var vehicle = await vehicleService.GetByIdAsync(vehicleId);

        if (vehicle != null)
            VehicleName = vehicle.DisplayName;

        FuelEntries.Clear();

        var entries = await fuelService.GetFuelEntriesByVehicleIdAsync(vehicleId);

        foreach (var entry in entries)
            FuelEntries.Add(entry);
    }
}