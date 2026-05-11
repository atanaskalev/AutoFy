using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class CalendarView : ContentPage
{
    public CalendarView(CalendarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}