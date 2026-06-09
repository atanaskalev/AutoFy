using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class ServiceHistoryViewModel : BaseViewModel, IQueryAttributable
{
    #region Fields

    private readonly IServiceRecordService serviceRecordService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;
    private string _vehicleName = string.Empty;

    #endregion

    #region Init

    public ServiceHistoryViewModel(IServiceRecordService serviceRecordService, IVehicleService vehicleService)
    {
        this.serviceRecordService = serviceRecordService;
        this.vehicleService = vehicleService;

        OpenEditServiceRecordCommand = new Command<ServiceRecordDto>(async serviceRecord =>
        {
            if (serviceRecord == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(Views.AddServiceRecordView)}?ServiceRecordId={serviceRecord.Id}");
        });

        Title = "История на сервиз";
    }

    #endregion

    #region Properties

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public ObservableCollection<ServiceRecordDto> ServiceRecords { get; } = new();

    #endregion

    #region Commands

    public ICommand OpenEditServiceRecordCommand { get; }

    #endregion

    #region Methods

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

    #endregion
}