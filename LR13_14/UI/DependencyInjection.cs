using LR13_14.UI.ViewModels;
using LR13_14.UI.ViewModels.Pages;
using LR13_14.UI.ViewModels.Pages.Routes;
using Microsoft.Extensions.DependencyInjection;

namespace UI;

public static class DependencyInjection
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<PageViewModelBase, HomeViewModel>();
        services.AddTransient<PageViewModelBase, RoomsViewModel>();
        services.AddTransient<ServiceDetailsViewModel>();
        services.AddTransient<AddCategoryViewModel>();
        services.AddTransient<AddServiceViewModel>();
        services.AddSingleton<MainWindowViewModel>();
        return services;
    }
}
