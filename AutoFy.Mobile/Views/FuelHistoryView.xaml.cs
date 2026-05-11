using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class FuelHistoryView : ContentPage
{
    public FuelHistoryView(FuelHistoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}