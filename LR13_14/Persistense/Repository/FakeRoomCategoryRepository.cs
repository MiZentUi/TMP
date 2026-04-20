using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Persistense.Repository;

public class FakeRoomCategoryRepository : IRepository<RoomCategory>
{
    readonly List<RoomCategory> _categories;

    public FakeRoomCategoryRepository()
    {
        _categories = [];
        var category = new RoomCategory("Standart", 1, 2)
        {
            Id = 1
        };
        _categories.Add(category);
        category = new RoomCategory("Deluxe", 1, 3)
        {
            Id = 2
        };
        _categories.Add(category);
    }

    public Task AddAsync(RoomCategory entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(RoomCategory entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RoomCategory> FirstOrDefaultAsync(Expression<Func<RoomCategory, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<RoomCategory> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<RoomCategory, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<RoomCategory>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _categories);
    }

    public Task<IReadOnlyList<RoomCategory>> ListAsync(Expression<Func<RoomCategory, bool>> filter, CancellationToken cancellationToken = default, params Expression<Func<RoomCategory, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RoomCategory entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}