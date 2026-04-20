using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LR13_14.Application.RoomCategoryUseCases.Commands;
using LR13_14.Application.ServiceUseCases.Commands;
using LR13_14.Domain.Entities;
using LR13_14.UI.ValueConverters;

namespace LR13_14.UI.ViewModels.Pages.Routes;

public partial class ServiceDetailsViewModel : RouteViewModelBase
{

    public Service? Service
    {
        get; set
        {
            if (value is not null)
            {
                Name = value.Data!.Name;
                Begin = value.Data!.Begin.ToTimeSpan();
                Duration = value.Data!.Duration;
                Cost = value.Cost;
                ImageBitmap = ServiceIdToImageConverter.Convert(value.Id);
            }
            field = value;
        }
    }

    private IMediator _mediator;

    [ObservableProperty]
    RoomCategory? _selectedCategory;

    public RoomCategory? Category { get; set; }

    public ObservableCollection<RoomCategory> Categories { get; set; } = [];

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private TimeSpan? _begin;

    [ObservableProperty]
    private TimeSpan? _duration;

    [ObservableProperty]
    private double? _cost;

    [ObservableProperty]
    private Brush _buttonBrush;

    [ObservableProperty]
    private Bitmap _imageBitmap;

    private Stream _imageStream;

    [RelayCommand]
    async Task UpdateCategoriesList() =>
       await GetCategories();

    public ServiceDetailsViewModel(IMediator mediator)
    {
        Title = "Service Details";

        _mediator = mediator;
        _buttonBrush = new SolidColorBrush(Colors.Transparent);
        _imageBitmap = ServiceIdToImageConverter.Convert(-1);
        _imageStream = new MemoryStream();
    }

    public async Task GetCategories()
    {
        var categories = await _mediator.Send(new GetAllCategoriesRequest());
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Categories.Clear();
            foreach (var category in categories)
                Categories.Add(category);
        });
        if (Category is not null)
        {
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == Category!.Id);
        }
    }

    [RelayCommand]
    async Task UpdateService()
    {
        if (Name is null || Begin is null || Duration is null || Cost is null || SelectedCategory is null || Service is null)
        {
            ButtonBrush = new SolidColorBrush(Colors.Red);
        }
        else
        {
            await _mediator.Send(new UpdateServiceCommand(Service, Name, TimeOnly.FromTimeSpan(Begin.Value), Duration.Value, Cost.Value, SelectedCategory.Id));
            ButtonBrush = new SolidColorBrush(Colors.Green);
            if (_imageStream.CanRead)
            {
                using var image = File.Create(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", Service.Id.ToString())));
                _imageStream.Position = 0;
                await _imageStream.CopyToAsync(image);
            }
        }
        await Dispatcher.UIThread.InvokeAsync(async () =>
        {
            await Task.Delay(3000);
            ButtonBrush = new SolidColorBrush(Colors.Transparent);
        });
    }

    [RelayCommand]
    private async Task SelectImage()
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        IStorageProvider? storageProvider = null;

        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            storageProvider = desktop.MainWindow?.StorageProvider;
        }
        else if (Avalonia.Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            storageProvider = TopLevel.GetTopLevel(singleView.MainView)?.StorageProvider;
        }

        if (storageProvider is null)
        {
            return;
        }

        // Start async operation to open the dialog.
        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open Text File",
            AllowMultiple = false,
            FileTypeFilter = [FilePickerFileTypes.ImageAll]
        });

        if (files.Count >= 1)
        {
            // Open reading stream from the first file.
            await using var stream = await files[0].OpenReadAsync();
            _imageStream.SetLength(0);
            await stream.CopyToAsync(_imageStream);
            _imageStream.Position = 0;
            stream.Seek(0, SeekOrigin.Begin);
            ImageBitmap = new Bitmap(stream);
        }
    }
}