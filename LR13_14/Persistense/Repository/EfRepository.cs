using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;
using LR13_14.Persistense.Data;
using Microsoft.EntityFrameworkCore;

namespace LR13_14.Persistense.Repository;

public class EfRepository<T> : IRepository<T> where T : Entity
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;

    public EfRepository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[]? includesProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        if (includesProperties!.Any())
        {
            foreach (Expression<Func<T, object>>? included in includesProperties!)
            {
                query = query.Include(included);
            }
        }
        query = query.Where(t => t.Id == id);
        return await query.FirstAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await _entities.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<T, object>>[]? includesProperties)
    {
        IQueryable<T>? query = _entities.AsQueryable();
        if (includesProperties!.Any())
        {
            foreach (Expression<Func<T, object>>? included in includesProperties!)
            {
                query = query.Include(included);
            }
        }
        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync(cancellationToken);
    }

    public Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Added;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var tracked = _entities.Local.FirstOrDefault(e => e.Id == entity.Id);
        if (tracked is not null && !ReferenceEquals(tracked, entity))
            _context.Entry(tracked).State = EntityState.Detached;

        _context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Deleted;
        return Task.CompletedTask;
    }

    public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return _entities.FirstOrDefaultAsync(filter, cancellationToken)!;
    }
}