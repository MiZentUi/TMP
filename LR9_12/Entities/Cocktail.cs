using SQLite;

[Table("Cocktails")]
public class Cocktail
{
    [PrimaryKey, AutoIncrement, Indexed]
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Volume { get; set; }
}
