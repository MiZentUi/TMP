using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LR13_14.Domain.Entities;

namespace LR13_14.Domain.Abstractions;

public interface IRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includesProperties);

    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties);

    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
}