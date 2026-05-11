using AutoFy.Mobile.ViewModels;

namespace AutoFy.Mobile.Views;

public partial class AddServiceRecordView : ContentPage
{
    public AddServiceRecordView(AddServiceRecordViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}