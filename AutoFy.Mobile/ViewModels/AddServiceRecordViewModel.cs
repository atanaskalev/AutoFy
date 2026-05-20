using AutoFy.Core.Enums;
using AutoFy.Core.Models;
using AutoFy.Services.Interfaces;
using System.Globalization;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddServiceRecordViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IServiceRecordService serviceRecordService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;

    private string _vehicleName = string.Empty;
    private string _odometer = string.Empty;
    private string _price = string.Empty;
    private string _notes = string.Empty;
    private string _serviceName = string.Empty;
    private string _serviceLocation = string.Empty;

    private DateTime _serviceDate = DateTime.Today;
    private ServiceType _selectedServiceType = ServiceType.Maintenance;

    public IEnumerable<ServiceType> ServiceTypes =>
        Enum.GetValues(typeof(ServiceType)).Cast<ServiceType>();

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public ServiceType SelectedServiceType
    {
        get => _selectedServiceType;
        set => SetProperty(ref _selectedServiceType, value);
    }

    public string Odometer
    {
        get => _odometer;
        set => SetProperty(ref _odometer, value);
    }

    public string Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public DateTime ServiceDate
    {
        get => _serviceDate;
        set => SetProperty(ref _serviceDate, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public string ServiceName
    {
        get => _serviceName;
        set => SetProperty(ref _serviceName, value);
    }

    public string ServiceLocation
    {
        get => _serviceLocation;
        set => SetProperty(ref _serviceLocation, value);
    }

    public ICommand SaveServiceCommand { get; }

    public AddServiceRecordViewModel(
        IServiceRecordService serviceRecordService,
        IVehicleService vehicleService)
    {
        this.serviceRecordService = serviceRecordService;
        this.vehicleService = vehicleService;

        Title = "Добави сервизна дейност";

        SaveServiceCommand = new Command(async () => await SaveServiceAsync());
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

    private async Task SaveServiceAsync()
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

        if (!TryParseDecimal(Price, out var parsedPrice) || parsedPrice < 0)
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Въведи валидна цена.", "OK");
            return;
        }

        var serviceRecord = new ServiceRecord
        {
            VehicleId = vehicleId,
            ServiceType = SelectedServiceType,
            Date = ServiceDate,
            Odometer = parsedOdometer,
            Price = parsedPrice,
            Description = Notes?.Trim(),
            ServiceName = ServiceName?.Trim(),
            ServiceLocation = ServiceLocation?.Trim()
        };

        await serviceRecordService.AddAsync(serviceRecord);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            "Сервизната дейност е записана успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
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