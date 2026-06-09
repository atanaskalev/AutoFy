using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AutoFy.Mobile.ViewModels;

public class CalendarViewModel : BaseViewModel
{
    #region Fields

    private readonly ICalendarService calendarService;

    private DateTime _selectedDate = DateTime.Today;

    private List<DateTime> eventDates = new();

    #endregion

    #region Init

    public CalendarViewModel(ICalendarService calendarService)
    {
        this.calendarService = calendarService;

        OpenEditReminderCommand = new Command<CalendarEventDto>(async calendarEvent =>
        {
            if (calendarEvent == null || !calendarEvent.ReminderId.HasValue)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(Views.AddReminderView)}?ReminderId={calendarEvent.ReminderId.Value}");
        });

        Title = "Календар";

        LoadEventsCommand = new Command(async () => await LoadEventsAsync());
    }

    #endregion

    #region Properties

    public DateTime SelectedDate
    {
        get => _selectedDate;
        set
        {
            if (SetProperty(ref _selectedDate, value))
                OnPropertyChanged(nameof(SelectedDateText));
        }
    }

    public List<DateTime> EventDates
    {
        get => eventDates;
        set => SetProperty(ref eventDates, value);
    }

    public string SelectedDateText =>
        SelectedDate.ToString("dd MMMM yyyy");

    public ObservableCollection<CalendarEventDto> CalendarEvents { get; } = new();

    public ObservableCollection<string> EventDateTexts { get; } = new();

    #endregion

    #region Commands

    public ICommand LoadEventsCommand { get; }
    public ICommand OpenEditReminderCommand { get; }

    #endregion

    #region Methods

    private async Task LoadEventsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            await calendarService.DeleteExpiredRemindersAsync();

            CalendarEvents.Clear();
            EventDateTexts.Clear();

            var events = await calendarService.GetAllActiveEventsAsync();

            foreach (var calendarEvent in events)
                CalendarEvents.Add(calendarEvent);

            var dates = await calendarService.GetEventDatesAsync();

            EventDates = dates
                .Select(x => x.Date)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            foreach (var date in EventDates)
                EventDateTexts.Add(date.ToString("dd.MM.yyyy"));
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}