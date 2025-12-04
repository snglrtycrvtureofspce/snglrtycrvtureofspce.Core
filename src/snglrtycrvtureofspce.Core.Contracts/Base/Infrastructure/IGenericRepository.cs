using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace snglrtycrvtureofspce.Core.Contracts.Base.Infrastructure;

/// <summary>
/// Universal repository for entities.
/// Provides basic CRUD operations and advanced query capabilities.
/// </summary>
public interface IGenericRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Asynchronously adds an entity to the repository.
    /// </summary>
    ValueTask AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the entity by identifier.
    /// </summary>
    ValueTask<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all entities.
    /// </summary>
    ValueTask<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the entity.
    /// </summary>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the entity.
    /// </summary>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the changes to the context.
    /// </summary>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
