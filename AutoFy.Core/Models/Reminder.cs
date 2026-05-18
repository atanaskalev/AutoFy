using AutoFy.Core.Base;
using AutoFy.Core.Enums;
using AutoFy.Core.Interfaces;

namespace AutoFy.Core.Models;

public class Reminder : BaseEntity, IReminder
{
    public int VehicleId { get; set; }

    public Vehicle? Vehicle { get; set; }

    public ReminderType ReminderType { get; set; }

    public DateTime ReminderDate { get; set; } = DateTime.Today;

    public string? Notes { get; set; }

    public bool IsCompleted { get; set; }
}