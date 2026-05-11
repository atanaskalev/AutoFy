using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddVehicleViewModel : BaseViewModel
{
    private string _brand = string.Empty;
    private string _model = string.Empty;
    private string _year = string.Empty;
    private string _licensePlate = string.Empty;
    private string _vin = string.Empty;
    private string _mileage = string.Empty;
    private string _fuelType = string.Empty;
    private string _transmissionType = string.Empty;
    private string _engineSize = string.Empty;
    private string _horsePower = string.Empty;

    private DateTime _technicalInspectionDate = DateTime.Today;
    private DateTime _insuranceDate = DateTime.Today;
    private DateTime _vignetteDate = DateTime.Today;
    private DateTime _fireExtinguisherDate = DateTime.Today;

    public string Brand
    {
        get => _brand;
        set => SetProperty(ref _brand, value);
    }

    public string Model
    {
        get => _model;
        set => SetProperty(ref _model, value);
    }

    public string Year
    {
        get => _year;
        set => SetProperty(ref _year, value);
    }

    public string LicensePlate
    {
        get => _licensePlate;
        set => SetProperty(ref _licensePlate, value);
    }

    public string Vin
    {
        get => _vin;
        set => SetProperty(ref _vin, value);
    }

    public string Mileage
    {
        get => _mileage;
        set => SetProperty(ref _mileage, value);
    }

    public string FuelType
    {
        get => _fuelType;
        set => SetProperty(ref _fuelType, value);
    }

    public string TransmissionType
    {
        get => _transmissionType;
        set => SetProperty(ref _transmissionType, value);
    }

    public string EngineSize
    {
        get => _engineSize;
        set => SetProperty(ref _engineSize, value);
    }

    public string HorsePower
    {
        get => _horsePower;
        set => SetProperty(ref _horsePower, value);
    }

    public DateTime TechnicalInspectionDate
    {
        get => _technicalInspectionDate;
        set => SetProperty(ref _technicalInspectionDate, value);
    }

    public DateTime InsuranceDate
    {
        get => _insuranceDate;
        set => SetProperty(ref _insuranceDate, value);
    }

    public DateTime VignetteDate
    {
        get => _vignetteDate;
        set => SetProperty(ref _vignetteDate, value);
    }

    public DateTime FireExtinguisherDate
    {
        get => _fireExtinguisherDate;
        set => SetProperty(ref _fireExtinguisherDate, value);
    }
    public ICommand SaveVehicleCommand { get; }

    public AddVehicleViewModel()
    {
        Title = "Добави автомобил";

        SaveVehicleCommand = new Command(async () =>
        {
            await Shell.Current.DisplayAlert("AutoFy", "Автомобилът ще бъде записан по-късно.", "OK");
        });
    }
}