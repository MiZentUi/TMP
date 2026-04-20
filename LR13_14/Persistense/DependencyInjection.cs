using LR13_14.Domain.Abstractions;
using LR13_14.Persistense.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LR13_14.Persistense;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWork, EfUnitOfWork>();
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, DbContextOptions options)
    {
        services.AddPersistence().AddSingleton(new AppDbContext((DbContextOptions<AppDbContext>)options));
        return services;
    }
}