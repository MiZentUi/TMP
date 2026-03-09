using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using LR9_11.ViewModels;
using ReactiveUI;
using Splat;

namespace LR9_11.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        ViewModel = AppLocator.Current.GetService<MainViewModel>() ?? new MainViewModel();
    }

    private void Menu(object sender, RoutedEventArgs args)
    {
        splitView.IsPaneOpen = !splitView.IsPaneOpen;
    }

    private void CloseMenu(object sender, RoutedEventArgs args)
    {
        splitView.IsPaneOpen = false;
    }
}