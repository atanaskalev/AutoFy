using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddServiceRecordViewModel : BaseViewModel
{
    private string _selectedVehicle = string.Empty;
    private string _serviceType = string.Empty;
    private string _odometer = string.Empty;
    private string _price = string.Empty;
    private string _notes = string.Empty;

    private DateTime _serviceDate = DateTime.Today;

    public string SelectedVehicle
    {
        get => _selectedVehicle;
        set => SetProperty(ref _selectedVehicle, value);
    }

    public string ServiceType
    {
        get => _serviceType;
        set => SetProperty(ref _serviceType, value);
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

    public ICommand SaveServiceCommand { get; }

    public AddServiceRecordViewModel()
    {
        Title = "Добави сервизна дейност";

        SaveServiceCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert(
                "AutoFy",
                "Сервизната дейност ще бъде записана по-късно.",
                "OK");
        });
    }
}