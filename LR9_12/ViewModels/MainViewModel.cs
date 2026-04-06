using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using LR9_12.ViewModels.Pages;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Splat;

namespace LR9_12.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [Reactive]
    public partial ObservableCollection<PageViewModelBase> Pages { get; set; }

    [Reactive]
    public partial PageViewModelBase CurrentPage { get; set; }

    [Reactive]
    public partial bool IsMenuOpen { get; set; }

    [Reactive]
    public partial ReactiveCommand<Unit, Unit> HideMenuCommand { get; set; }

    public MainViewModel(IEnumerable<PageViewModelBase>? pages = null)
    {
        Pages = [];

        foreach (var page in pages ?? Locator.Current.GetServices<PageViewModelBase>())
        {
            Pages.Add(page);
        }

        CurrentPage = Pages[0];
        IsMenuOpen = false;

        HideMenuCommand = ReactiveCommand.Create(HideMenu);
    }

    private void HideMenu()
    {
        IsMenuOpen = false;
        System.Console.WriteLine("Test");
    }
}