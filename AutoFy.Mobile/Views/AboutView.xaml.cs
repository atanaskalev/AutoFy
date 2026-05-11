using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class AboutView : ContentPage
{
    public AboutView(AboutViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}