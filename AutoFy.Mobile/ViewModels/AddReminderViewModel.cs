using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddReminderViewModel : BaseViewModel
{
    private string _selectedVehicle = string.Empty;
    private string _reminderType = string.Empty;
    private string _notes = string.Empty;

    private DateTime _reminderDate = DateTime.Today;

    public string SelectedVehicle
    {
        get => _selectedVehicle;
        set => SetProperty(ref _selectedVehicle, value);
    }

    public string ReminderType
    {
        get => _reminderType;
        set => SetProperty(ref _reminderType, value);
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

    public ICommand SaveReminderCommand { get; }

    public AddReminderViewModel()
    {
        Title = "Добави напомняне";

        SaveReminderCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert(
                "AutoFy",
                "Напомнянето ще бъде записано по-късно.",
                "OK");
        });
    }
}