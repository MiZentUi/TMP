using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;

namespace LR13_14.Persistense.Repository;

public class FakeServiceRepository : IRepository<Service>
{
    readonly List<Service> _list = [];

    public FakeServiceRepository()
    {
        int k = 1;
        for (int i = 1; i <= 2; i++)
            for (int j = 0; j < 10; j++)
            {
                var _service = new Service(new ServiceData($"Service {k++}", TimeOnly.FromDateTime(DateTime.Now), TimeSpan.FromMinutes(k)), Random.Shared.NextDouble() * 10);
                _service.AddToRoomCategory(i);
                _list.Add(_service);
            }
    }

    public Task AddAsync(Service entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Service entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Service> FirstOrDefaultAsync(Expression<Func<Service, bool>> filter, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Service> GetByIdAsync(int id, CancellationToken cancellationToken = default, params Expression<Func<Service, object>>[]? includesProperties)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Service>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => _list);
    }

    public async Task<IReadOnlyList<Service>> ListAsync(Expression<Func<Service, bool>> filter,
        CancellationToken cancellationToken = default,
        params Expression<Func<Service, object>>[]? includesProperties)
    {
        var data = _list.AsQueryable();
        return [.. data.Where(filter)];
    }

    public Task UpdateAsync(Service entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}