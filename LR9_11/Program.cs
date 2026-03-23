using Avalonia;
using System;
using Splat;
using ReactiveUI.Avalonia;
using System.Reflection;
using LR9_11.ViewModels;
using LR9_11.ViewModels.Pages;

namespace LR9_11;

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
                        // Register your services here
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new HomeViewModel());
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new CalculatorViewModel());
                        locator.RegisterLazySingleton<PageViewModelBase>(() => new ProgressViewModel());
                        locator.RegisterLazySingleton(() => new MainViewModel(Locator.Current.GetServices<PageViewModelBase>()));
                    });
            }).RegisterReactiveUIViewsFromEntryAssembly();
}
