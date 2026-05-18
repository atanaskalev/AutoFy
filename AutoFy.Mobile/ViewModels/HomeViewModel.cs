using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IVehicleService vehicleService;

        public ObservableCollection<VehicleDto> Vehicles { get; } = new();

        public int VehiclesCount => Vehicles.Count;

        public ICommand OpenAboutCommand { get; }
        public ICommand OpenMenuCommand { get; }
        public ICommand OpenVehicleDetailsCommand { get; }
        public ICommand OpenAddVehicleCommand { get; }

        public ICommand LoadVehiclesCommand { get; }

        public HomeViewModel(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;

            Title = "AutoFy";

            OpenAboutCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.AboutView)));

            OpenMenuCommand = new Command(() =>
                Shell.Current.FlyoutIsPresented = true);

            OpenVehicleDetailsCommand = new Command<VehicleDto>(async vehicle =>
            {
                if (vehicle == null)
                    return;

                await Shell.Current.GoToAsync(
                    $"{nameof(Views.VehicleDetailsView)}?VehicleId={vehicle.Id}");
            });

            OpenAddVehicleCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.AddVehicleView)));

            LoadVehiclesCommand = new Command(async () =>
                await LoadVehiclesAsync());
        }

        private async Task LoadVehiclesAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                Vehicles.Clear();

                var vehicles = await vehicleService.GetAllAsync();

                foreach (var vehicle in vehicles)
                    Vehicles.Add(vehicle);

                OnPropertyChanged(nameof(VehiclesCount));
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}