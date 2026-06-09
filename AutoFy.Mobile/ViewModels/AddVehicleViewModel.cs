using AutoFy.Core.Enums;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class AddVehicleViewModel : BaseViewModel, IQueryAttributable
{
    #region Fields

    private readonly IVehicleService vehicleService;

    private int? vehicleId;
    private string? imagePath;

    private string _brand = string.Empty;
    private string _model = string.Empty;
    private string _year = string.Empty;
    private string _licensePlate = string.Empty;
    private string _vin = string.Empty;
    private string _mileage = string.Empty;
    private string _selectedFuelType = string.Empty;
    private string _selectedTransmissionType = string.Empty;
    private string _engineSize = string.Empty;
    private string _horsePower = string.Empty;
    private string _vignettePrice = string.Empty;
    private string _insurancePrice = string.Empty;
    private string _technicalInspectionPrice = string.Empty;
    private string _fireExtinguisherPrice = string.Empty;

    private DateTime _technicalInspectionDate = DateTime.Today;
    private DateTime _insuranceDate = DateTime.Today;
    private DateTime _vignetteDate = DateTime.Today;
    private DateTime _fireExtinguisherDate = DateTime.Today;

    #endregion

    #region Init

    public AddVehicleViewModel(IVehicleService vehicleService)
    {
        this.vehicleService = vehicleService;

        Title = "Добави автомобил";

        SaveVehicleCommand = new Command(async () => await SaveVehicleAsync());
        PickImageCommand = new Command(async () => await PickImageAsync());
        RemoveImageCommand = new Command(RemoveImage);
    }

    #endregion

    #region Properties

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

    public string SelectedFuelType
    {
        get => _selectedFuelType;
        set => SetProperty(ref _selectedFuelType, value);
    }

    public string SelectedTransmissionType
    {
        get => _selectedTransmissionType;
        set => SetProperty(ref _selectedTransmissionType, value);
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

    public string? ImagePath
    {
        get => imagePath;
        set
        {
            if (SetProperty(ref imagePath, value))
            {
                OnPropertyChanged(nameof(HasImage));
            }
        }
    }

    public string VignettePrice
    {
        get => _vignettePrice;
        set => SetProperty(ref _vignettePrice, value);
    }

    public string InsurancePrice
    {
        get => _insurancePrice;
        set => SetProperty(ref _insurancePrice, value);
    }

    public string TechnicalInspectionPrice
    {
        get => _technicalInspectionPrice;
        set => SetProperty(ref _technicalInspectionPrice, value);
    }

    public string FireExtinguisherPrice
    {
        get => _fireExtinguisherPrice;
        set => SetProperty(ref _fireExtinguisherPrice, value);
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

    public bool HasImage => !string.IsNullOrWhiteSpace(ImagePath);

    public string SaveButtonText => vehicleId.HasValue
        ? "Запази промените"
        : "Добави автомобил";

    #endregion

    #region Commands

    public ICommand SaveVehicleCommand { get; }
    public ICommand PickImageCommand { get; }
    public ICommand RemoveImageCommand { get; }

    #endregion

    #region Methods

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("VehicleId", out var vehicleIdValue))
            return;

        if (!int.TryParse(vehicleIdValue?.ToString(), out var id))
            return;

        vehicleId = id;
        Title = "Редактирай автомобил";
        OnPropertyChanged(nameof(SaveButtonText));

        await LoadVehicleAsync(id);
    }

    private async Task LoadVehicleAsync(int id)
    {
        var vehicle = await vehicleService.GetByIdAsync(id);

        if (vehicle == null)
            return;

        Brand = vehicle.Brand;
        Model = vehicle.Model;
        Year = vehicle.Year.ToString();
        LicensePlate = vehicle.LicensePlate;
        Vin = vehicle.Vin;
        Mileage = vehicle.Mileage.ToString();
        SelectedFuelType = MapFuelTypeToText(vehicle.FuelType);
        SelectedTransmissionType = MapTransmissionTypeToText(vehicle.TransmissionType);
        EngineSize = vehicle.EngineSize;
        HorsePower = vehicle.HorsePower.ToString();
        ImagePath = vehicle.ImagePath;
        VignettePrice = vehicle.VignettePrice.ToString("F2");
        InsurancePrice = vehicle.InsurancePrice.ToString("F2");
        TechnicalInspectionPrice = vehicle.TechnicalInspectionPrice.ToString("F2");
        FireExtinguisherPrice = vehicle.FireExtinguisherPrice.ToString("F2");
        TechnicalInspectionDate = vehicle.TechnicalInspectionDate ?? DateTime.Today;
        InsuranceDate = vehicle.InsuranceDate ?? DateTime.Today;
        VignetteDate = vehicle.VignetteDate ?? DateTime.Today;
        FireExtinguisherDate = vehicle.FireExtinguisherDate ?? DateTime.Today;
    }

    private async Task SaveVehicleAsync()
    {
        if (string.IsNullOrWhiteSpace(Brand) || string.IsNullOrWhiteSpace(Model))
        {
            await Shell.Current.DisplayAlertAsync(
                "Грешка",
                "Моля, въведи марка и модел.",
                "OK");

            return;
        }

        int.TryParse(Year, out var parsedYear);
        int.TryParse(Mileage, out var parsedMileage);
        int.TryParse(HorsePower, out var parsedHorsePower);
        decimal.TryParse(VignettePrice, out var parsedVignettePrice);
        decimal.TryParse(InsurancePrice, out var parsedInsurancePrice);
        decimal.TryParse(TechnicalInspectionPrice, out var parsedTechnicalInspectionPrice);
        decimal.TryParse(FireExtinguisherPrice, out var parsedFireExtinguisherPrice);

        var vehicleDto = new VehicleDto
        {
            Id = vehicleId ?? 0,
            Brand = Brand.Trim(),
            Model = Model.Trim(),
            Year = parsedYear,
            LicensePlate = LicensePlate.Trim(),
            Vin = Vin.Trim(),
            Mileage = parsedMileage,
            FuelType = MapFuelType(SelectedFuelType),
            TransmissionType = MapTransmissionType(SelectedTransmissionType),
            EngineSize = EngineSize.Trim(),
            HorsePower = parsedHorsePower,
            ImagePath = ImagePath,
            VignettePrice = parsedVignettePrice,
            InsurancePrice = parsedInsurancePrice,
            TechnicalInspectionPrice = parsedTechnicalInspectionPrice,
            FireExtinguisherPrice = parsedFireExtinguisherPrice,
            TechnicalInspectionDate = TechnicalInspectionDate,
            InsuranceDate = InsuranceDate,
            VignetteDate = VignetteDate,
            FireExtinguisherDate = FireExtinguisherDate
        };

        if (vehicleId.HasValue)
            await vehicleService.UpdateAsync(vehicleDto);
        else
            await vehicleService.AddAsync(vehicleDto);

        await Shell.Current.DisplayAlertAsync(
            "AutoFy",
            vehicleId.HasValue
                ? "Автомобилът е редактиран успешно."
                : "Автомобилът е добавен успешно.",
            "OK");

        await Shell.Current.GoToAsync("..");
    }

    private async Task PickImageAsync()
    {
        var photo = await MediaPicker.PickPhotoAsync();

        if (photo == null)
            return;

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
        var destinationPath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        await using var sourceStream = await photo.OpenReadAsync();
        await using var destinationStream = File.OpenWrite(destinationPath);

        await sourceStream.CopyToAsync(destinationStream);

        ImagePath = destinationPath;
    }

    private static FuelType MapFuelType(string value)
    {
        return value switch
        {
            "Бензин" => FuelType.Бензин,
            "Дизел" => FuelType.Дизел,
            "Газ" => FuelType.Газ,
            "Хибрид" => FuelType.Хибрид,
            "Електрически" => FuelType.Електрически,
            _ => FuelType.Бензин
        };
    }

    private static TransmissionType MapTransmissionType(string value)
    {
        return value switch
        {
            "Автоматична" => TransmissionType.Автоматична,
            "Ръчна" => TransmissionType.Ръчна,
            _ => TransmissionType.Ръчна
        };
    }

    private static string MapFuelTypeToText(FuelType fuelType)
    {
        return fuelType switch
        {
            FuelType.Бензин => "Бензин",
            FuelType.Дизел => "Дизел",
            FuelType.Газ => "Газ",
            FuelType.Хибрид => "Хибрид",
            FuelType.Електрически => "Електрически",
            _ => "Бензин"
        };
    }

    private static string MapTransmissionTypeToText(TransmissionType transmissionType)
    {
        return transmissionType switch
        {
            TransmissionType.Автоматична => "Автоматична",
            TransmissionType.Ръчна => "Ръчна",
            _ => "Ръчна"
        };
    }

    private void RemoveImage()
    {
        ImagePath = null;
    }

    #endregion
}