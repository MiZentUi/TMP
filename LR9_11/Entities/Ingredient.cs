using SQLite;

[Table("Ingredients")]
public class Ingredient
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Strength { get; set; }
    [Indexed]
    public int CocktailId { get; set; }
}