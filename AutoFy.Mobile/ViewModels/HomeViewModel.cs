using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ICommand OpenAboutCommand { get; }
        public ICommand OpenMenuCommand { get; }
        public ICommand OpenVehicleDetailsCommand { get; }

        public ICommand OpenAddVehicleCommand { get; }

        public HomeViewModel()
        {
            Title = "AutoFy";

            OpenAboutCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.AboutView)));

            OpenMenuCommand = new Command(() =>
                Shell.Current.FlyoutIsPresented = true);

            OpenVehicleDetailsCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.VehicleDetailsView)));

            OpenAddVehicleCommand = new Command(async () =>
                await Shell.Current.GoToAsync(nameof(Views.AddVehicleView)));
        }
    }
}
