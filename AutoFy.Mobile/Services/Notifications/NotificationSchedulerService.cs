using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace AutoFy.Mobile.Services.Notifications
{
    public class NotificationSchedulerService : INotificationSchedulerService
    {
        #region Fields

        private const int ReminderBase = 100000;
        private const int VehicleDeadlineBase = 200000;

        #endregion

        #region Methods

        public async Task RequestPermissionAsync()
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }

        public async Task ScheduleReminderNotificationsAsync(int reminderId, DateTime reminderDate, string title, string vehicleName)
        {
            await CancelReminderNotificationsAsync(reminderId);

            var previousDay = DateTime.Now.AddSeconds(10);
            var sameDay = DateTime.Now.AddSeconds(20);

            await ScheduleAsync(
                GetReminderNotificationId(reminderId, 1),
                "Предстоящо напомняне",
                $"{title} за {vehicleName} предстои на {reminderDate:dd.MM.yyyy}.",
                previousDay);

            await ScheduleAsync(
                GetReminderNotificationId(reminderId, 2),
                "Напомняне за днес",
                $"{title} за {vehicleName} е насрочено за днес.",
                sameDay);
        }

        public async Task CancelReminderNotificationsAsync(int reminderId)
        {
            LocalNotificationCenter.Current.Cancel(GetReminderNotificationId(reminderId, 1));
            LocalNotificationCenter.Current.Cancel(GetReminderNotificationId(reminderId, 2));

            await Task.CompletedTask;
        }

        public async Task ScheduleVehicleDeadlineNotificationsAsync(int vehicleId, DateTime? date, string deadlineName, string vehicleName)
        {
            if (!date.HasValue)
                return;

            await CancelVehicleDeadlineNotificationsAsync(vehicleId, deadlineName);

            var previousDay = DateTime.Now.AddSeconds(10);
            var sameDay = DateTime.Now.AddSeconds(20);

            await ScheduleAsync(
                GetVehicleDeadlineNotificationId(vehicleId, deadlineName, 1),
                "Предстоящ срок",
                $"{deadlineName} на {vehicleName} е валидно до {date.Value:dd.MM.yyyy}.",
                previousDay);

            await ScheduleAsync(
                GetVehicleDeadlineNotificationId(vehicleId, deadlineName, 2),
                "Срокът е днес",
                $"{deadlineName} на {vehicleName} изтича днес.",
                sameDay);
        }

        public async Task CancelVehicleDeadlineNotificationsAsync(int vehicleId, string deadlineKey)
        {
            LocalNotificationCenter.Current.Cancel(GetVehicleDeadlineNotificationId(vehicleId, deadlineKey, 1));
            LocalNotificationCenter.Current.Cancel(GetVehicleDeadlineNotificationId(vehicleId, deadlineKey, 2));

            await Task.CompletedTask;
        }

        private static async Task ScheduleAsync(int id, string title, string description, DateTime notifyTime)
        {
            if (notifyTime <= DateTime.Now)
                return;

            var request = new NotificationRequest
            {
                NotificationId = id,
                Title = title,
                Description = description,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notifyTime,
                    NotifyRepeatInterval = TimeSpan.Zero
                }
            };

            await LocalNotificationCenter.Current.Show(request);
        }

        private static int GetReminderNotificationId(int reminderId, int offset)
        {
            return ReminderBase + reminderId * 10 + offset;
        }

        private static int GetVehicleDeadlineNotificationId(int vehicleId, string deadlineKey, int offset)
        {
            var deadlineOffset = deadlineKey switch
            {
                "Гражданска отговорност" => 1,
                "Винетка" => 2,
                "Технически преглед" => 3,
                "Пожарогасител" => 4,
                _ => 9
            };

            return VehicleDeadlineBase + vehicleId * 100 + deadlineOffset * 10 + offset;
        }

        #endregion
    }
}
