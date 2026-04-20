using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LR13_14.UI.ViewModels.Pages;

namespace LR13_14.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<PageViewModelBase> _pages;

    [ObservableProperty]
    private PageViewModelBase _selectedPage;

    [ObservableProperty]
    private PageViewModelBase _currentPage;

    private Stack<PageViewModelBase> _pageHistory = new();

    [ObservableProperty]
    private bool _isMenuOpen;

    [ObservableProperty]
    private string _buttonText;

    [RelayCommand]
    void Navigation() => NavigationHandler();

    [RelayCommand]
    void PageChanged() => SetPage();

    public MainWindowViewModel(IEnumerable<PageViewModelBase>? pages = null)
    {
        _pages = [];

        foreach (var page in pages!)
        {
            _pages.Add(page);
        }

        _buttonText = "\u2261";
        _selectedPage = _pages[0];
        _currentPage = _selectedPage;
        SetPage();
    }

    private void NavigationHandler()
    {
        if (_pageHistory.Count != 0)
        {
            CurrentPage = _pageHistory.Pop();
            if (_pageHistory.Count == 0)
            {
                ButtonText = "\u2261";
            }
        }
        else
        {
            IsMenuOpen = true;
        }
    }

    public void SetPage()
    {
        CurrentPage = SelectedPage;
        IsMenuOpen = false;
    }

    public void NextPage(PageViewModelBase page)
    {
        _pageHistory.Push(CurrentPage);
        ButtonText = "\u2190";
        CurrentPage = page;
    }
}
