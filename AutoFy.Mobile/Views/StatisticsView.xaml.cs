using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class StatisticsView : ContentPage
{
    public StatisticsView(StatisticsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}