using System;
using System.Threading.Tasks;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;
using LR13_14.Persistense.Repository;

namespace LR13_14.Persistense;

public class FakeUnitOfWork : IUnitOfWork
{
    private readonly Lazy<IRepository<RoomCategory>> _roomCategoryRepository;
    private readonly Lazy<IRepository<Service>> _serviceRepository;

    public FakeUnitOfWork()
    {
        _roomCategoryRepository = new Lazy<IRepository<RoomCategory>>(() =>
            new FakeRoomCategoryRepository());
        _serviceRepository = new Lazy<IRepository<Service>>(() =>
            new FakeServiceRepository());
    }

    public IRepository<RoomCategory> RoomCategoryRepository =>
        _roomCategoryRepository.Value;

    public IRepository<Service> ServiceRepository =>
        _serviceRepository.Value;

    public Task CreateDataBaseAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteDataBaseAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveAllAsync()
    {
        throw new NotImplementedException();
    }
}