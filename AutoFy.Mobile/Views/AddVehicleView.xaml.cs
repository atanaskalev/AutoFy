using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class AddVehicleView : ContentPage
{
    public AddVehicleView(AddVehicleViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}