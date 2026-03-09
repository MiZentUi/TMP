using Avalonia;
using LR9_11.ViewModels;
using ReactiveUI;
using ReactiveUI.Avalonia;
using System;
using System.Reflection;
using Avalonia.ReactiveUI;

namespace LR9_11;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
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
                      locator.RegisterLazySingleton<IScreen>(() => new MainViewModel());
                      //   locator.RegisterLazySingleton<INavigationService>(() => new NavigationService());
                  });
            }).RegisterReactiveUIViewsFromEntryAssembly();
}
