using System.Collections.ObjectModel;

namespace AutoFy.Mobile.ViewModels;

public class CalendarViewModel : BaseViewModel
{
    private DateTime _selectedDate = DateTime.Today;

    private ObservableCollection<object> _calendarEvents = [];

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (SetProperty(ref _selectedDate, value))
            {
                OnPropertyChanged(nameof(SelectedDateText));
            }
        }
    }

    public string SelectedDateText =>
        SelectedDate.ToString("dd MMMM yyyy");

    public ObservableCollection<object> CalendarEvents
    {
        get => _calendarEvents;
        set => SetProperty(ref _calendarEvents, value);
    }

    public CalendarViewModel()
    {
        Title = "Календар";
    }
}