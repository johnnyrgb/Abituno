﻿namespace DAL.Interfaces
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>?> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
