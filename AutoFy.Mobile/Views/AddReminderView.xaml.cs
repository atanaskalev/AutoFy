using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class AddReminderView : ContentPage
{
    public AddReminderView(AddReminderViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}