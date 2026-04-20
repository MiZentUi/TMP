using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LR13_14.Application.ServiceUseCases.Commands;
using LR13_14.Domain.Entities;
using LR13_14.UI.ValueConverters;

namespace LR13_14.UI.ViewModels.Pages.Routes;

public partial class AddServiceViewModel : RouteViewModelBase
{
    private IMediator _mediator;

    public RoomCategory? Category { get; set; }

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

    public AddServiceViewModel(IMediator mediator)
    {
        Title = "Add Service";
        _mediator = mediator;
        _buttonBrush = new SolidColorBrush(Colors.Transparent);
        _imageBitmap = ServiceIdToImageConverter.Convert(-1);
        _imageStream = new MemoryStream();
    }

    [RelayCommand]
    async Task AddService()
    {
        if (Name is null || Begin is null || Duration is null || Cost is null || Category is null)
        {
            ButtonBrush = new SolidColorBrush(Colors.Red);
        }
        else
        {
            var service = await _mediator.Send(new AddServiceCommand(Name, TimeOnly.FromTimeSpan(Begin.Value), Duration.Value, Cost.Value, Category.Id));
            ButtonBrush = new SolidColorBrush(Colors.Green);
            if (_imageStream.CanRead)
            {
                using var image = File.Create(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", service.Id.ToString())));
                _imageStream.Position = 0;
                await _imageStream.CopyToAsync(image);
            }
        }
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