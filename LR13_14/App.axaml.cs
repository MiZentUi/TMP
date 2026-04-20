using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UI;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using LR13_14.Application;
using LR13_14.Persistense.Data;
using LR13_14.Persistense;
using LR13_14.UI.ViewModels;
using LR13_14.UI.Views;

namespace LR13_14;

public partial class App : Avalonia.Application
{
    public static MainWindowViewModel? MainViewModel { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // If you use CommunityToolkit, line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }


        var connStr = configuration.GetConnectionString("PostgreSQLConnection");
        var options = new DbContextOptionsBuilder<AppDbContext>().UseNpgsql(connStr).Options;

        // Register all the services needed for the application to run
        var collection = new ServiceCollection();
        collection.AddLogging();
        collection.AddApplication();
        collection.AddPersistence(options);
        collection.AddViewModels();

        // Creates a ServiceProvider containing services from the provided IServiceCollection
        var services = collection.BuildServiceProvider();

        System.Threading.Tasks.Task.Run(() => DbInitializer.Initialize(services)).GetAwaiter().GetResult();

        var vm = services.GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = vm
            };
        }

        MainViewModel = vm;

        base.OnFrameworkInitializationCompleted();
    }
}