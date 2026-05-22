using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.Interfaces;

namespace AutoFy.Services.Services;

public class ReminderService : IReminderService
{
    private readonly IReminderRepository reminderRepository;

    public ReminderService(IReminderRepository reminderRepository)
    {
        this.reminderRepository = reminderRepository;
    }

    public async Task<IEnumerable<Reminder>> GetAllAsync()
    {
        return await reminderRepository.GetAllAsync();
    }

    public async Task AddAsync(Reminder reminder)
    {
        await reminderRepository.AddAsync(reminder);
    }

    public async Task CompleteAsync(Reminder reminder)
    {
        reminder.IsCompleted = true;
        reminder.UpdatedAt = DateTime.Now;

        await reminderRepository.UpdateAsync(reminder);
    }

    public async Task<Reminder?> GetByIdAsync(int id)
    {
        return await reminderRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Reminder reminder)
    {
        reminder.UpdatedAt = DateTime.Now;

        await reminderRepository.UpdateAsync(reminder);
    }

    public async Task DeleteAsync(int id)
    {
        var reminder = await reminderRepository.GetByIdAsync(id);

        if (reminder == null)
            return;

        await reminderRepository.DeleteAsync(reminder);
    }
}