using AutoFy.Core.Enums;

namespace AutoFy.Core.Interfaces;

public interface IReminder : IEntity
{
    int VehicleId { get; set; }

    ReminderType ReminderType { get; set; }

    DateTime ReminderDate { get; set; }

    bool IsCompleted { get; set; }
}