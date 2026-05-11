namespace AutoFy.Mobile.ViewModels;

public class StatisticsViewModel : BaseViewModel
{
    private string _totalVehicles = "2";
    private string _totalFuelCost = "2 160 лв";
    private string _totalServiceCost = "1 480 лв";
    private string _totalCost = "3 640 лв";
    private string _averageFuelConsumption = "6.4 л/100 км";
    private string _costPerKilometer = "0.21 лв/км";
    private string _mostExpensiveVehicle = "BMW 320d";
    private string _lastActivity = "Смяна на масло";

    public string TotalVehicles
    {
        get => _totalVehicles;
        set => SetProperty(ref _totalVehicles, value);
    }

    public string TotalFuelCost
    {
        get => _totalFuelCost;
        set => SetProperty(ref _totalFuelCost, value);
    }

    public string TotalServiceCost
    {
        get => _totalServiceCost;
        set => SetProperty(ref _totalServiceCost, value);
    }

    public string TotalCost
    {
        get => _totalCost;
        set => SetProperty(ref _totalCost, value);
    }

    public string AverageFuelConsumption
    {
        get => _averageFuelConsumption;
        set => SetProperty(ref _averageFuelConsumption, value);
    }

    public string CostPerKilometer
    {
        get => _costPerKilometer;
        set => SetProperty(ref _costPerKilometer, value);
    }

    public string MostExpensiveVehicle
    {
        get => _mostExpensiveVehicle;
        set => SetProperty(ref _mostExpensiveVehicle, value);
    }

    public string LastActivity
    {
        get => _lastActivity;
        set => SetProperty(ref _lastActivity, value);
    }

    public StatisticsViewModel()
    {
        Title = "Статистика";
    }
}