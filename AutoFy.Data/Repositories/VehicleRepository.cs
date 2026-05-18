using AutoFy.Core.Models;
using AutoFy.Data.Context;
using AutoFy.Data.Repositories.Base;
using AutoFy.Data.Repositories.Interfaces;

namespace AutoFy.Data.Repositories.Implementations;

public class VehicleRepository
    : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(AutoFyDbContext context)
        : base(context)
    {
    }
}