using AutoFy.Core.Enums;
using AutoFy.Core.Models;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Globalization;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddServiceRecordViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IServiceRecordService serviceRecordService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;
    private int? serviceRecordId;

    private string _vehicleName = string.Empty;
    private string _odometer = string.Empty;
    private string _price = string.Empty;
    private string _notes = string.Empty;
    private string _serviceName = string.Empty;
    private string _serviceLocation = string.Empty;

    private DateTime _serviceDate = DateTime.Today;
    private ServiceType _selectedServiceType = ServiceType.Обслужване;

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

    public string SaveButtonText =>
        serviceRecordId.HasValue ? "Запази промените" : "Запази дейността";

    public bool IsEditMode => serviceRecordId.HasValue;

    public ICommand SaveServiceCommand { get; }
    public ICommand DeleteServiceCommand { get; }

    public AddServiceRecordViewModel(
        IServiceRecordService serviceRecordService,
        IVehicleService vehicleService)
    {
        this.serviceRecordService = serviceRecordService;
        this.vehicleService = vehicleService;

        Title = "Добави сервизна дейност";

        SaveServiceCommand = new Command(async () => await SaveServiceAsync());
        DeleteServiceCommand = new Command(async () => await DeleteServiceAsync());
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ServiceRecordId", out var serviceRecordIdValue) &&
            int.TryParse(serviceRecordIdValue?.ToString(), out var parsedServiceRecordId))
        {
            serviceRecordId = parsedServiceRecordId;
            Title = "Редактирай сервизна дейност";

            OnPropertyChanged(nameof(SaveButtonText));
            OnPropertyChanged(nameof(IsEditMode));

            await LoadServiceRecordAsync(parsedServiceRecordId);
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

    private async Task LoadServiceRecordAsync(int id)
    {
        var serviceRecord = await serviceRecordService.GetByIdAsync(id);

        if (serviceRecord == null)
            return;

        vehicleId = serviceRecord.VehicleId;

        var vehicle = await vehicleService.GetByIdAsync(vehicleId);

        if (vehicle != null)
            VehicleName = vehicle.DisplayName;

        SelectedServiceType = serviceRecord.ServiceType;
        ServiceDate = serviceRecord.Date;
        Odometer = serviceRecord.Odometer.ToString();
        Price = serviceRecord.Price.ToString("F2");
        Notes = serviceRecord.Description ?? string.Empty;
        ServiceName = serviceRecord.ServiceName ?? string.Empty;
        ServiceLocation = serviceRecord.ServiceLocation ?? string.Empty;
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

        if (serviceRecordId.HasValue)
        {
            var dto = new ServiceRecordDto
            {
                Id = serviceRecordId.Value,
                VehicleId = vehicleId,
                ServiceType = SelectedServiceType,
                Date = ServiceDate,
                Odometer = parsedOdometer,
                Price = parsedPrice,
                Description = Notes?.Trim(),
                ServiceName = ServiceName?.Trim(),
                ServiceLocation = ServiceLocation?.Trim()
            };

            await serviceRecordService.UpdateAsync(dto);
        }
        else
        {
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
        }

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            serviceRecordId.HasValue
                ? "Сервизната дейност е редактирана успешно."
                : "Сервизната дейност е записана успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }

    private async Task DeleteServiceAsync()
    {
        if (!serviceRecordId.HasValue)
            return;

        var confirmed = await Shell.Current.DisplayAlertAsync(
            "Изтриване",
            "Сигурен ли си, че искаш да изтриеш тази сервизна дейност?",
            "Да",
            "Не");

        if (!confirmed)
            return;

        await serviceRecordService.DeleteAsync(serviceRecordId.Value);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            "Сервизната дейност е изтрита успешно.",
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