using Avalonia.Controls;
using Avalonia.Interactivity;
using LR9_12.ViewModels;
using ReactiveUI.Avalonia;
using Splat;

namespace LR9_12.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        ViewModel = Locator.Current.GetService<MainViewModel>();
    }

    private void Menu(object sender, RoutedEventArgs args)
    {
        splitView.IsPaneOpen = !splitView.IsPaneOpen;
    }

    private void CloseMenu(object sender, SelectionChangedEventArgs args)
    {
        splitView.IsPaneOpen = false;
    }
}