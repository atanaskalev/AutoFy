using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class HomeView : ContentPage
{
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}