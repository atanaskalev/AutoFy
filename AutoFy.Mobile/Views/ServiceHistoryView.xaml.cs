using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class ServiceHistoryView : ContentPage
{
    public ServiceHistoryView(ServiceHistoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}