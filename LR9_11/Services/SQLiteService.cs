using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

public class SQLiteService : IDbService
{
    private SQLiteConnection? db;

    public void Init()
    {
        var databasePath = Path.Combine(Environment.CurrentDirectory, "pub.db");
        var databaseExists = File.Exists(databasePath);
        db = new SQLiteConnection(databasePath);
        if (databaseExists)
        {
            return;
        }

        db.CreateTable<Cocktail>();
        db.CreateTable<Ingredient>();

        AddCocktailWithIngredients(
            new()
            {
                Name = "Bloody Mary",
                Volume = 0.15
            },
            [
                new()
                {
                    Name = "Vodka",
                    Strength = 40
                },
                new()
                {
                    Name = "Tomato juice",
                    Strength = 0
                },
                new()
                {
                    Name = "Lemon juice",
                    Strength = 0
                },
                new()
                {
                    Name = "Hot sauce",
                    Strength = 0
                },
                new()
                {
                    Name = "Olives",
                    Strength = 0
                }
            ]
        );

        AddCocktailWithIngredients(
            new()
            {
                Name = "Hangman's blood",
                Volume = 0.17
            },
            [
                new()
                {
                    Name = "Gin",
                    Strength = 50
                },
                new()
                {
                    Name = "Whisky",
                    Strength = 40
                },
                new()
                {
                    Name = "Rum",
                    Strength = 60
                },
                new()
                {
                    Name = "Port",
                    Strength = 20
                },
                new()
                {
                    Name = "Stout",
                    Strength = 5
                },
                new()
                {
                    Name = "Champagne",
                    Strength = 11
                }
            ]
        );

        AddCocktailWithIngredients(
            new()
            {
                Name = "Long Island iced tea",
                Volume = 0.15
            },
            [
                new()
                {
                    Name = "Vodka",
                    Strength = 40
                },
                new()
                {
                    Name = "Tequila",
                    Strength = 45
                },
                new()
                {
                    Name = "Light rum",
                    Strength = 60
                },
                new()
                {
                    Name = "Triple sec",
                    Strength = 30
                },
                new()
                {
                    Name = "Gin",
                    Strength = 50
                },
                new()
                {
                    Name = "Cola",
                    Strength = 0
                }
            ]
        );

        AddCocktailWithIngredients(
            new()
            {
                Name = "Stas's cocktail",
                Volume = 0.2
            },
            [
                new()
                {
                    Name = "Vodka",
                    Strength = 40
                },
                new()
                {
                    Name = "Grape Fanta",
                    Strength = 45
                }
            ]
        );
    }

    private void AddCocktailWithIngredients(Cocktail cocktail, IEnumerable<Ingredient> ingredients)
    {
        db!.Insert(cocktail);
        foreach (var item in ingredients)
        {
            item.CocktailId = cocktail.Id;
        }
        db.InsertAll(ingredients);
    }

    public IEnumerable<Cocktail> GetAllCoctails()
    {
        return [.. db!.Table<Cocktail>()];
    }

    public IEnumerable<Ingredient> GetCocktailIngredients(int id)
    {
        return [.. db!.Table<Ingredient>().Where(i => i.CocktailId == id)];
    }
}