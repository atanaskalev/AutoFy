using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class ProfileView : ContentPage
{
    public ProfileView(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}