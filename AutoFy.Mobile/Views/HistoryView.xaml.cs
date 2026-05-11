using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class HistoryView : ContentPage
{
    public HistoryView(HistoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}