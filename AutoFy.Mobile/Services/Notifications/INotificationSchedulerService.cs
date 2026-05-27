namespace AutoFy.Mobile.Services.Notifications
{
    public interface INotificationSchedulerService
    {
        Task RequestPermissionAsync();

        Task ScheduleReminderNotificationsAsync(
            int reminderId,
            DateTime reminderDate,
            string title,
            string vehicleName);

        Task CancelReminderNotificationsAsync(int reminderId);

        Task ScheduleVehicleDeadlineNotificationsAsync(
            int vehicleId,
            DateTime? date,
            string deadlineName,
            string vehicleName);

        Task CancelVehicleDeadlineNotificationsAsync(
            int vehicleId,
            string deadlineKey);
    }
}
