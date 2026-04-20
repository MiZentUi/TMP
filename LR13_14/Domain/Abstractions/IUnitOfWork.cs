using System.Threading.Tasks;
using LR13_14.Domain.Entities;

namespace LR13_14.Domain.Abstractions;

public interface IUnitOfWork
{
    IRepository<RoomCategory> RoomCategoryRepository { get; }
    IRepository<Service> ServiceRepository { get; }
    public Task SaveAllAsync();
    public Task DeleteDataBaseAsync();
    public Task CreateDataBaseAsync();
}