using AutoFy.Core.Interfaces;
using AutoFy.Data.Context;
using AutoFy.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoFy.Data.Repositories.Base;

public class Repository<T> : IRepository<T>
    where T : class, IEntity
{
    protected readonly AutoFyDbContext context;

    protected readonly DbSet<T> dbSet;

    public Repository(AutoFyDbContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        dbSet.Update(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        dbSet.Remove(entity);
        await context.SaveChangesAsync();
    }
}