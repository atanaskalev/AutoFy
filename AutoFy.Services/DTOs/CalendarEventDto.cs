namespace AutoFy.Services.DTOs;

public class CalendarEventDto
{
    public int? ReminderId { get; set; }

    public bool IsReminder => ReminderId.HasValue;

    public DateTime Date { get; set; }

    public string VehicleName { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public string DateText => Date.ToString("dd.MM.yyyy");

    public bool HasNotes => !string.IsNullOrWhiteSpace(Notes);
}