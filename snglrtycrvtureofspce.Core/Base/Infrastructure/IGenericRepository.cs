using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace snglrtycrvtureofspce.Core.Base.Infrastructure;

public interface IGenericRepository<T> where T : class, IEntity
{
    Task<T> GetByIdAsync(Guid id);
    
    Task<IEnumerable<T>> GetAllAsync();
    
    Task AddAsync(T entity);
    
    Task UpdateAsync(T entity);
    
    Task DeleteAsync(T entity);
    
    Task SaveChangesAsync();
}