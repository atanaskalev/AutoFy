using AutoFy.Core.Enums;
using AutoFy.Core.Models;
using AutoFy.Data.Repositories.Interfaces;
using AutoFy.Services.Services;
using Moq;

namespace AutoFy.Tests.Services;

public class ReminderServiceTests
{
    private readonly Mock<IReminderRepository> reminderRepositoryMock;
    private readonly ReminderService reminderService;

    public ReminderServiceTests()
    {
        reminderRepositoryMock = new Mock<IReminderRepository>();

        reminderService = new ReminderService(
            reminderRepositoryMock.Object);
    }

    [Fact]
    public async Task AddAsync_ShouldCallRepository()
    {
        var reminder = new Reminder
        {
            VehicleId = 1,
            ReminderType = ReminderType.Друго,
            ReminderDate = DateTime.Today.AddDays(7),
            Notes = "Смяна масло"
        };

        await reminderService.AddAsync(reminder);

        reminderRepositoryMock.Verify(
            x => x.AddAsync(reminder),
            Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllReminders()
    {
        var reminders = new List<Reminder>
        {
            new()
            {
                Id = 1,
                VehicleId = 1,
                ReminderType = ReminderType.Друго
            },
            new()
            {
                Id = 2,
                VehicleId = 1,
                ReminderType = ReminderType.Друго
            }
        };

        reminderRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(reminders);

        var result = await reminderService.GetAllAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task CompleteAsync_ShouldMarkReminderAsCompleted()
    {
        var reminder = new Reminder
        {
            Id = 1,
            IsCompleted = false
        };

        await reminderService.CompleteAsync(reminder);

        Assert.True(reminder.IsCompleted);

        reminderRepositoryMock.Verify(
            x => x.UpdateAsync(reminder),
            Times.Once);
    }

    [Fact]
    public async Task CompleteAsync_ShouldSetUpdatedAt()
    {
        var reminder = new Reminder
        {
            Id = 1,
            IsCompleted = false
        };

        await reminderService.CompleteAsync(reminder);

        Assert.NotNull(reminder.UpdatedAt);
    }
}