using AutoFy.Services.DTOs;

namespace AutoFy.Services.Interfaces;

public interface ICalendarService
{
    Task DeleteExpiredRemindersAsync();

    Task<IEnumerable<CalendarEventDto>> GetAllActiveEventsAsync();

    Task<IEnumerable<DateTime>> GetEventDatesAsync();
}