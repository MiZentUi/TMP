using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LR9_11.ViewModels;

public partial class MainViewModel : ViewModelBase, IScreen
{
    private readonly HomeViewModel _homePage;
    private readonly CalculatorViewModel _calculatorPage;

    [Reactive]
    private ViewModelBase _currentPage;

    public RoutingState Router => throw new System.NotImplementedException();

    public MainViewModel()
    {
        _homePage = new();
        _calculatorPage = new();

        CurrentPage = _homePage;
    }

    [RelayCommand]
    private void GoHome()
    {
        CurrentPage = _homePage;
    }

    [RelayCommand]
    private void GoCalculator()
    {
        CurrentPage = _calculatorPage;
    }
}