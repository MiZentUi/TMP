using System;
using System.Threading.Tasks;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;
using LR13_14.Persistense.Data;
using LR13_14.Persistense.Repository;

namespace LR13_14.Persistense;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly Lazy<IRepository<RoomCategory>> _roomCategoryRepository;
    private readonly Lazy<IRepository<Service>> _serviceRepository;

    public EfUnitOfWork(AppDbContext context)
    {
        _context = context;
        _roomCategoryRepository = new Lazy<IRepository<RoomCategory>>(() =>
            new EfRepository<RoomCategory>(context));
        _serviceRepository = new Lazy<IRepository<Service>>(() =>
            new EfRepository<Service>(context));
    }

    public IRepository<RoomCategory> RoomCategoryRepository =>
        _roomCategoryRepository.Value;

    public IRepository<Service> ServiceRepository =>
        _serviceRepository.Value;

    public async Task CreateDataBaseAsync() =>
        await _context.Database.EnsureCreatedAsync();

    public async Task DeleteDataBaseAsync() =>
        await _context.Database.EnsureDeletedAsync();

    public async Task SaveAllAsync() =>
        await _context.SaveChangesAsync();
}