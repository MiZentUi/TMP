using System;
using System.Threading.Tasks;
using LR13_14.Domain.Abstractions;
using LR13_14.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace LR13_14.Application;

public static class DbInitializer
{
    public static async Task Initialize(IServiceProvider services)
    {
        var unitOfWork = services.GetRequiredService<IUnitOfWork>();

        await unitOfWork.DeleteDataBaseAsync();
        await unitOfWork.CreateDataBaseAsync();

        var categoryRepository = unitOfWork.RoomCategoryRepository;

        await categoryRepository.AddAsync(new RoomCategory("Standart", 1, 2));
        await categoryRepository.AddAsync(new RoomCategory("Deluxe", 2, 3));
        await categoryRepository.AddAsync(new RoomCategory("Luxe", 3, 5));

        await unitOfWork.SaveAllAsync();

        var serviceRepository = unitOfWork.ServiceRepository;

        var standart = await categoryRepository.FirstOrDefaultAsync(t => t.Name!.ToLower().Equals("standart"));
        await AddService(serviceRepository, standart, new Service(new ServiceData("Cleaning", TimeOnly.FromTimeSpan(TimeSpan.FromHours(17)), TimeSpan.FromHours(1)), 10));
        await AddService(serviceRepository, standart, new Service(new ServiceData("Breakfast", TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)), TimeSpan.FromHours(2)), 20));

        var deluxe = await categoryRepository.FirstOrDefaultAsync(t => t.Name!.ToLower().Equals("deluxe"));
        await AddService(serviceRepository, deluxe, new Service(new ServiceData("Cleaning", TimeOnly.FromTimeSpan(TimeSpan.FromHours(14)), TimeSpan.FromHours(1)), 20));
        await AddService(serviceRepository, deluxe, new Service(new ServiceData("Breakfast", TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)), TimeSpan.FromHours(2)), 25));
        await AddService(serviceRepository, deluxe, new Service(new ServiceData("Gym", TimeOnly.FromTimeSpan(TimeSpan.FromHours(10)), TimeSpan.FromHours(8)), 40));

        var luxe = await categoryRepository.FirstOrDefaultAsync(t => t.Name!.ToLower().Equals("luxe"));
        await AddService(serviceRepository, luxe, new Service(new ServiceData("Cleaning", TimeOnly.FromTimeSpan(TimeSpan.FromHours(12)), TimeSpan.FromHours(2)), 50));
        await AddService(serviceRepository, luxe, new Service(new ServiceData("Breakfast", TimeOnly.FromTimeSpan(TimeSpan.FromHours(9)), TimeSpan.FromHours(2)), 30));
        await AddService(serviceRepository, luxe, new Service(new ServiceData("Lunch", TimeOnly.FromTimeSpan(TimeSpan.FromHours(13)), TimeSpan.FromHours(2)), 40));
        await AddService(serviceRepository, luxe, new Service(new ServiceData("Dinner", TimeOnly.FromTimeSpan(TimeSpan.FromHours(19)), TimeSpan.FromHours(2)), 50));
        await AddService(serviceRepository, luxe, new Service(new ServiceData("Mini-Bar", TimeOnly.FromTimeSpan(TimeSpan.FromHours(0)), TimeSpan.FromHours(24)), 100));

        await unitOfWork.SaveAllAsync();
    }

    private static async Task AddService(IRepository<Service> serviceRepository, RoomCategory category, Service service)
    {
        service.AddToRoomCategory(category.Id);
        await serviceRepository.AddAsync(service);
    }
}