using System;
using System.Reactive;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Threading;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LR9_11.ViewModels.Pages;

public partial class ProgressViewModel : PageViewModelBase
{
    [Reactive]
    public partial int Progress { get; set; }

    [Reactive]
    public partial string Status { get; set; }

    public ReactiveCommand<Unit, Unit> StartCommand { get; }
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }

    [Reactive]
    private partial bool IsCalculating { get; set; }

    private CancellationTokenSource cancellationTokenSource;

    public ProgressViewModel()
    {
        Title = "Progress";

        Status = "";
        Progress = 0;
        IsCalculating = false;
        cancellationTokenSource = new CancellationTokenSource();

        StartCommand = ReactiveCommand.CreateFromTask(Start);
        CancelCommand = ReactiveCommand.Create(Cancel, this.WhenAnyValue(x => x.IsCalculating));

        StartCommand.IsExecuting.Subscribe(isExecuting => IsCalculating = isExecuting);
    }

    private async Task<double?> Calculate(CancellationToken token)
    {
        double a = 0.0;
        double b = 1.0;
        double step = 0.00000001;
        double result = 0;
        for (double i = a; i < b; i += step)
        {
            result += Math.Sin(i + step / 2) * step;
            for (int j = 0; j < 100; j++)
            {
                result *= 1;
            }

            if (token.IsCancellationRequested)
            {
                return null;
            }

            Dispatcher.UIThread.Post(() =>
            {
                if (Dispatcher.UIThread.CheckAccess())
                {
                    Progress = (int)Math.Ceiling((i - step) / (b - a) * 100);
                }
            });
        }
        return result;
    }

    private async Task Start()
    {
        Status = "Calculating...";
        cancellationTokenSource.Cancel();
        cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        var result = await Task.Run(() => Calculate(cancellationToken), cancellationToken);
        if (result != null)
        {
            Status = "Result: " + result.ToString();
        }
    }

    private void Cancel()
    {
        Status = "Task Canceled";
        cancellationTokenSource.Cancel();
    }
}