using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Collections.ObjectModel;

namespace AutoFy.Mobile.ViewModels;

public class ServiceHistoryViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IServiceRecordService serviceRecordService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;
    private string _vehicleName = string.Empty;

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public ObservableCollection<ServiceRecordDto> ServiceRecords { get; } = new();

    public ServiceHistoryViewModel(
        IServiceRecordService serviceRecordService,
        IVehicleService vehicleService)
    {
        this.serviceRecordService = serviceRecordService;
        this.vehicleService = vehicleService;

        Title = "История на сервиз";
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

        ServiceRecords.Clear();

        var records = await serviceRecordService.GetServiceRecordsByVehicleIdAsync(vehicleId);

        foreach (var record in records)
            ServiceRecords.Add(record);
    }
}