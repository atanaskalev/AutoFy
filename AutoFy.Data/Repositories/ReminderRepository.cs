using AutoFy.Core.Models;
using AutoFy.Data.Context;
using AutoFy.Data.Repositories.Base;
using AutoFy.Data.Repositories.Interfaces;

namespace AutoFy.Data.Repositories.Implementations;

public class ReminderRepository
    : Repository<Reminder>, IReminderRepository
{
    public ReminderRepository(AutoFyDbContext context)
        : base(context)
    {
    }
}