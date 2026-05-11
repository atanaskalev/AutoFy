using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class VehicleDetailsView : ContentPage
{
    public VehicleDetailsView(VehicleDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}