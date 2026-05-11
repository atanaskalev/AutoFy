using System.Collections.ObjectModel;

namespace AutoFy.Mobile.ViewModels;

public class FuelHistoryViewModel : BaseViewModel
{
    private string _vehicleName = "BMW 320d";

    private ObservableCollection<object> _fuelEntries = [];

    public string VehicleName
    {
        get => _vehicleName;
        set => SetProperty(ref _vehicleName, value);
    }

    public ObservableCollection<object> FuelEntries
    {
        get => _fuelEntries;
        set => SetProperty(ref _fuelEntries, value);
    }

    public FuelHistoryViewModel()
    {
        Title = "История на зареждания";
    }
}