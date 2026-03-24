using Splat;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections;
using System.Collections.Generic;
using DynamicData.Binding;
using System.Reactive.Linq;
using System.Reactive;

namespace LR9_11.ViewModels.Pages;

public partial class PubViewModel : PageViewModelBase
{
    [Reactive]
    public partial List<Cocktail> Cocktails { get; set; }

    [Reactive]
    public partial IEnumerable<Ingredient> Ingredients { get; set; }

    public Cocktail CurrentCocktail
    {
        get;
        set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            OnCocktailChanged();
        }
    }

    private readonly IDbService? _dbService;

    public PubViewModel(IDbService? dbService = null)
    {
        _dbService = dbService ?? Locator.Current.GetService<IDbService>()!;
        _dbService.Init();

        Title = "Pub";
        Cocktails = [.. _dbService.GetAllCoctails()];
        CurrentCocktail = Cocktails[0];
        Ingredients = _dbService.GetCocktailIngredients(CurrentCocktail.Id);

        // this.WhenAnyValue(vm => vm.CurrentCocktail).Where(item => item != null).Subscribe(new AnonymousObserver<Cocktail>(OnCocktailChanged));
    }

    public void OnCocktailChanged()
    {
        Ingredients = _dbService!.GetCocktailIngredients(CurrentCocktail.Id);
    }
}