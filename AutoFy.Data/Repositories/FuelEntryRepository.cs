using AutoFy.Core.Models;
using AutoFy.Data.Context;
using AutoFy.Data.Repositories.Base;
using AutoFy.Data.Repositories.Interfaces;

namespace AutoFy.Data.Repositories.Implementations;

public class FuelEntryRepository
    : Repository<FuelEntry>, IFuelEntryRepository
{
    public FuelEntryRepository(AutoFyDbContext context)
        : base(context)
    {
    }
}