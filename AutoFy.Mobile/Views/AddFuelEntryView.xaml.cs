using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class AddFuelEntryView : ContentPage
{
    public AddFuelEntryView(AddFuelEntryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}