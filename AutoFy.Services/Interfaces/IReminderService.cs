using AutoFy.Core.Models;

namespace AutoFy.Services.Interfaces;

public interface IReminderService
{
    Task<IEnumerable<Reminder>> GetAllAsync();

    Task AddAsync(Reminder reminder);

    Task CompleteAsync(Reminder reminder);

    Task<Reminder?> GetByIdAsync(int id);

    Task UpdateAsync(Reminder reminder);

    Task DeleteAsync(int id);
}