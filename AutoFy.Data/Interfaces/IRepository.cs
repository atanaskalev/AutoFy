using AutoFy.Core.Interfaces;

namespace AutoFy.Data.Repositories.Interfaces;

public interface IRepository<T>
    where T : class, IEntity
{
    Task<T?> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    Task AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}