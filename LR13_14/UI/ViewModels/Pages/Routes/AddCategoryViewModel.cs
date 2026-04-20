using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LR13_14.Application.RoomCategoryUseCases.Commands;
using LR13_14.Domain.Entities;

namespace LR13_14.UI.ViewModels.Pages.Routes;

public partial class AddCategoryViewModel : RouteViewModelBase
{
    private IMediator _mediator;

    public RoomCategory? Category { get; set; }

    [ObservableProperty]
    private string? _name;

    [ObservableProperty]
    private int? _roomsCount;

    [ObservableProperty]
    private int? _starsCount;

    [ObservableProperty]
    private Brush _buttonBrush;

    public AddCategoryViewModel(IMediator mediator)
    {
        Title = "Add Room Category";

        _mediator = mediator;
        _buttonBrush = new SolidColorBrush(Colors.Transparent);
    }

    [RelayCommand]
    async Task AddCategory()
    {
        if (Name is null || RoomsCount is null || StarsCount is null)
        {
            ButtonBrush = new SolidColorBrush(Colors.Red);
        }
        else
        {
            await _mediator.Send(new AddCategoryCommand(Name, RoomsCount.Value, StarsCount.Value));
            ButtonBrush = new SolidColorBrush(Colors.Green);
        }
    }
}