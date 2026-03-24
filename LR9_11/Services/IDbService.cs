using System.Collections.Generic;

public interface IDbService
{
    void Init();
    IEnumerable<Cocktail> GetAllCoctails();
    IEnumerable<Ingredient> GetCocktailIngredients(int id);
}