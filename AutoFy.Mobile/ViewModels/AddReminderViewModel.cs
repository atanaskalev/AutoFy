using AutoFy.Core.Enums;
using AutoFy.Core.Models;
using AutoFy.Services.Interfaces;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddReminderViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IReminderService reminderService;
    private readonly IVehicleService vehicleService;

    private int vehicleId;
    private int? reminderId;

    private string _vehicleName = string.Empty;
    private ReminderType _selectedReminderType = ReminderType.Друго;
    private string _notes = string.Empty;
    private DateTime _reminderDate = DateTime.Today;

    public IEnumerable<ReminderType> ReminderTypes =>
        Enum.GetValues(typeof(ReminderType)).Cast<ReminderType>();

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public ReminderType SelectedReminderType
    {
        get => _selectedReminderType;
        set => SetProperty(ref _selectedReminderType, value);
    }

    public string Notes
    {
        get => _notes;
        set => SetProperty(ref _notes, value);
    }

    public DateTime ReminderDate
    {
        get => _reminderDate;
        set => SetProperty(ref _reminderDate, value);
    }

    public string SaveButtonText =>
        reminderId.HasValue ? "Запази промените" : "Запази напомнянето";

    public bool IsEditMode => reminderId.HasValue;

    public ICommand SaveReminderCommand { get; }
    public ICommand DeleteReminderCommand { get; }

    public AddReminderViewModel(
        IReminderService reminderService,
        IVehicleService vehicleService)
    {
        this.reminderService = reminderService;
        this.vehicleService = vehicleService;

        Title = "Добави напомняне";

        SaveReminderCommand = new Command(async () => await SaveReminderAsync());
        DeleteReminderCommand = new Command(async () => await DeleteReminderAsync());
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ReminderId", out var reminderIdValue) &&
            int.TryParse(reminderIdValue?.ToString(), out var parsedReminderId))
        {
            reminderId = parsedReminderId;
            Title = "Редактирай напомняне";

            OnPropertyChanged(nameof(SaveButtonText));
            OnPropertyChanged(nameof(IsEditMode));

            await LoadReminderAsync(parsedReminderId);
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

    private async Task LoadReminderAsync(int id)
    {
        var reminder = await reminderService.GetByIdAsync(id);

        if (reminder == null)
            return;

        vehicleId = reminder.VehicleId;

        var vehicle = await vehicleService.GetByIdAsync(vehicleId);

        if (vehicle != null)
            VehicleName = vehicle.DisplayName;

        SelectedReminderType = reminder.ReminderType;
        ReminderDate = reminder.ReminderDate;
        Notes = reminder.Notes ?? string.Empty;
    }

    private async Task SaveReminderAsync()
    {
        if (vehicleId <= 0)
        {
            await Shell.Current.DisplayAlertAsync("Грешка", "Не е избран автомобил.", "OK");
            return;
        }

        if (reminderId.HasValue)
        {
            var reminder = await reminderService.GetByIdAsync(reminderId.Value);

            if (reminder == null)
                return;

            reminder.VehicleId = vehicleId;
            reminder.ReminderType = SelectedReminderType;
            reminder.ReminderDate = ReminderDate;
            reminder.Notes = Notes?.Trim();
            reminder.IsCompleted = false;

            await reminderService.UpdateAsync(reminder);
        }
        else
        {
            var reminder = new Reminder
            {
                VehicleId = vehicleId,
                ReminderType = SelectedReminderType,
                ReminderDate = ReminderDate,
                Notes = Notes?.Trim(),
                IsCompleted = false
            };

            await reminderService.AddAsync(reminder);
        }

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            reminderId.HasValue
                ? "Напомнянето е редактирано успешно."
                : "Напомнянето е записано успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }

    private async Task DeleteReminderAsync()
    {
        if (!reminderId.HasValue)
            return;

        var confirmed = await Shell.Current.DisplayAlertAsync(
            "Изтриване",
            "Сигурен ли си, че искаш да изтриеш това напомняне?",
            "Да",
            "Не");

        if (!confirmed)
            return;

        await reminderService.DeleteAsync(reminderId.Value);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            "Напомнянето е изтрито успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }
}