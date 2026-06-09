using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.DTOs;
using AutoFy.Services.Interfaces;

namespace AutoFy.Services.Services;

public class CalendarService : ICalendarService
{
    #region Fields

    private readonly IVehicleRepository vehicleRepository;
    private readonly IReminderRepository reminderRepository;

    #endregion

    #region Init

    public CalendarService(IVehicleRepository vehicleRepository,  IReminderRepository reminderRepository)
    {
        this.vehicleRepository = vehicleRepository;
        this.reminderRepository = reminderRepository;
    }

    #endregion

    #region Methods

    public async Task DeleteExpiredRemindersAsync()
    {
        var reminders = await reminderRepository.GetAllAsync();

        var expiredReminders = reminders
            .Where(x => x.ReminderDate.Date < DateTime.Today)
            .ToList();

        foreach (var reminder in expiredReminders)
            await reminderRepository.DeleteAsync(reminder);
    }

    public async Task<IEnumerable<CalendarEventDto>> GetAllActiveEventsAsync()
    {
        var vehicles = (await vehicleRepository.GetAllAsync()).ToList();
        var reminders = (await reminderRepository.GetAllAsync()).ToList();

        var result = new List<CalendarEventDto>();

        foreach (var vehicle in vehicles)
        {
            var vehicleName = $"{vehicle.Brand} {vehicle.Model}";

            AddVehicleEvent(result, vehicle.TechnicalInspectionDate, vehicleName, "Технически преглед");
            AddVehicleEvent(result, vehicle.InsuranceDate, vehicleName, "Гражданска отговорност");
            AddVehicleEvent(result, vehicle.VignetteDate, vehicleName, "Винетка");
            AddVehicleEvent(result, vehicle.FireExtinguisherDate, vehicleName, "Пожарогасител");
        }

        var reminderEvents = reminders
            .Where(x => !x.IsCompleted && x.ReminderDate.Date >= DateTime.Today)
            .Select(x =>
            {
                var vehicle = vehicles.FirstOrDefault(v => v.Id == x.VehicleId);

                return new CalendarEventDto
                {
                    ReminderId = x.Id,
                    Date = x.ReminderDate,
                    VehicleName = vehicle == null ? "Неизвестен автомобил" : $"{vehicle.Brand} {vehicle.Model}",
                    Title = x.ReminderType.ToString(),
                    Type = "Напомняне",
                    Notes = x.Notes
                };
            });

        result.AddRange(reminderEvents);

        return result
            .Where(x => x.Date.Date >= DateTime.Today)
            .OrderBy(x => x.Date)
            .ToList();
    }

    public async Task<IEnumerable<DateTime>> GetEventDatesAsync()
    {
        var vehicles = await vehicleRepository.GetAllAsync();
        var reminders = await reminderRepository.GetAllAsync();

        var dates = new List<DateTime>();

        foreach (var vehicle in vehicles)
        {
            AddDate(dates, vehicle.TechnicalInspectionDate);
            AddDate(dates, vehicle.InsuranceDate);
            AddDate(dates, vehicle.VignetteDate);
            AddDate(dates, vehicle.FireExtinguisherDate);
        }

        dates.AddRange(
            reminders
                .Where(x => !x.IsCompleted && x.ReminderDate.Date >= DateTime.Today)
                .Select(x => x.ReminderDate.Date));

        return dates
            .Distinct()
            .OrderBy(x => x)
            .ToList();
    }

    private static void AddVehicleEvent(
        List<CalendarEventDto> result,
        DateTime? date,
        string vehicleName,
        string title)
    {
        if (!date.HasValue)
            return;

        result.Add(new CalendarEventDto
        {
            Date = date.Value,
            VehicleName = vehicleName,
            Title = title,
            Type = "Годишен срок",
            Notes = null
        });
    }

    private static void AddDate(List<DateTime> dates, DateTime? date)
    {
        if (date.HasValue && date.Value.Date >= DateTime.Today)
            dates.Add(date.Value.Date);
    }

    #endregion
}