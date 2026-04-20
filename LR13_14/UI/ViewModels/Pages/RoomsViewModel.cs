using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LR13_14.Application.RoomCategoryUseCases.Commands;
using LR13_14.Application.ServiceUseCases.Commands;
using LR13_14.Domain.Entities;
using LR13_14.UI.ViewModels.Pages.Routes;

namespace LR13_14.UI.ViewModels.Pages;

public partial class RoomsViewModel : PageViewModelBase
{
    private readonly IMediator _mediator;
    private readonly ServiceDetailsViewModel _serviceDetailsViewModel;
    private readonly AddCategoryViewModel _addCategoryViewModel;
    private readonly AddServiceViewModel _addServiceViewModel;

    public ObservableCollection<RoomCategory> Categories { get; set; } = [];
    public ObservableCollection<Service> Services { get; set; } = [];

    [ObservableProperty]
    RoomCategory? _selectedCategory;

    private RoomCategory? _currentCategory;

    [ObservableProperty]
    Service? _selectedService;

    [ObservableProperty]
    bool? _isAddServiceButton;

    [RelayCommand]
    async Task UpdateCategoriesList() =>
        await GetCategories();

    [RelayCommand]
    async Task UpdateMembersList() =>
        await GetServices();

    [RelayCommand]
    async Task ShowDetails(Service service) =>
        await GotoDetailsPage(service);

    [RelayCommand]
    async Task AddCategory() =>
        await GotoAddCategoryPage();

    [RelayCommand]
    async Task AddService() =>
        await GotoAddServicePage();

    public RoomsViewModel(IMediator mediator,
        ServiceDetailsViewModel serviceDetailsViewModel,
        AddServiceViewModel addServiceViewModel,
        AddCategoryViewModel addCategoryViewModel)
    {
        Title = "Rooms";
        Icon = "\uf236";

        _mediator = mediator;
        _serviceDetailsViewModel = serviceDetailsViewModel;
        _addServiceViewModel = addServiceViewModel;
        _addCategoryViewModel = addCategoryViewModel;

        _isAddServiceButton = false;
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
        if (_currentCategory is not null)
        {
            SelectedCategory = Categories.FirstOrDefault(c => c.Id == _currentCategory!.Id);
        }
        else
        {
            IsAddServiceButton = false;
        }
    }

    public async Task GetServices()
    {
        var services = await _mediator.Send(new GetServicesByCategoryRequest(SelectedCategory!.Id));
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Services.Clear();
            foreach (var service in services)
                Services.Add(service);
        });
        _currentCategory = SelectedCategory;
        if (_currentCategory is not null)
        {
            IsAddServiceButton = true;
        }
    }

    public async Task GotoDetailsPage(Service service)
    {
        _serviceDetailsViewModel.Service = service;
        _serviceDetailsViewModel.Category = SelectedCategory;
        App.MainViewModel!.NextPage(_serviceDetailsViewModel);
    }

    public async Task GotoAddCategoryPage()
    {
        App.MainViewModel!.NextPage(_addCategoryViewModel);
    }

    public async Task GotoAddServicePage()
    {
        _addServiceViewModel.Category = SelectedCategory;
        App.MainViewModel!.NextPage(_addServiceViewModel);
    }
}
