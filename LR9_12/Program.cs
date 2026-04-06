using Avalonia;
using System;
using Splat;
using ReactiveUI.Avalonia;
using System.Reflection;
using LR9_12.ViewModels;
using LR9_12.ViewModels.Pages;
using System.Net.Http;

namespace LR9_12;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) =>
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI(rxAppBuilder =>
            {
                // Enable ReactiveUI
                rxAppBuilder
                    .WithViewsFromAssembly(Assembly.GetExecutingAssembly())
                    .WithRegistration(locator =>
                    {
                        var resolver = Locator.Current;

                        locator.RegisterLazySingleton(() => new HttpClient());

                        // Register your services here
                        locator.Register<IDbService, SQLiteService>();
                        locator.Register<IRateService>(() => new RateService(resolver.GetService<HttpClient>()));

                        locator.RegisterLazySingleton<PageViewModelBase>(() => new HomeViewModel());
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new CalculatorViewModel());
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new ProgressViewModel());
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new PubViewModel(resolver.GetService<IDbService>()));
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new CurrencyConverterViewModel(resolver.GetService<IRateService>()));
                        locator.RegisterLazySingleton(() => new MainViewModel(resolver.GetServices<PageViewModelBase>()));
                    });
            }).RegisterReactiveUIViewsFromEntryAssembly();
}
