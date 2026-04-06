using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData.Binding;
using NbrbAPI.Models;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Splat;

namespace LR9_12.ViewModels.Pages;

public partial class CurrencyConverterViewModel : PageViewModelBase
{
    private readonly IRateService? _rateService;

    public DateTimeOffset Date
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Rates = [.. _rateService!.GetRates(value.DateTime)];
            FromRate = Rates[0];
            ToRate = Rates[0];
            Convert();
        }
    }

    [Reactive]
    public partial DateTimeOffset MaxDate { get; set; }

    [Reactive]
    public partial ObservableCollection<Rate> Rates { get; set; }

    public Rate FromRate
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Convert(true);
        }
    }

    public Rate ToRate
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            Convert();
        }
    }

    private bool _isConverting;

    public decimal? FromValue
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            if (!_isConverting)
            {
                _isConverting = true;
                Convert();
                _isConverting = false;
            }
        }
    }

    public decimal? ToValue
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            if (!_isConverting)
            {
                _isConverting = true;
                Convert(true);
                _isConverting = false;
            }
        }
    }

    public CurrencyConverterViewModel(IRateService? rateService = null)
    {
        _rateService = rateService ?? Locator.Current.GetService<IRateService>();

        Title = "Currency Converter";
        MaxDate = DateTimeOffset.Now;
        Date = DateTimeOffset.Now;
        Rates = [.. _rateService!.GetRates(Date.DateTime)];
        FromRate = Rates[0];
        ToRate = Rates[0];
        FromValue = 1;
        ToValue = 1;
    }

    private void Convert(bool reverse = false)
    {
        if (FromValue is null || ToValue is null || FromRate is null || ToRate is null)
        {
            return;
        }

        if (!reverse)
        {
            ToValue = FromValue * ToRate.Cur_OfficialRate / FromRate.Cur_OfficialRate;
        }
        else
        {
            FromValue = ToValue * FromRate.Cur_OfficialRate / ToRate.Cur_OfficialRate;
        }
    }
}