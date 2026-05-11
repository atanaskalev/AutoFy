using System.Collections.ObjectModel;

namespace AutoFy.Mobile.ViewModels;

public class HistoryViewModel : BaseViewModel
{
    private ObservableCollection<object> _historyItems = [];

    public ObservableCollection<object> HistoryItems
    {
        get => _historyItems;
        set => SetProperty(ref _historyItems, value);
    }

    public HistoryViewModel()
    {
        Title = "История";
    }
}