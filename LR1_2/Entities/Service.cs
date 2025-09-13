
namespace LR1_2.Entities
{
	internal class Service(string _name, decimal _price, Tariff _tariff)
	{
		public string Name { get => _name; }
		public Tariff Tariff { get => _tariff; }
		public decimal Price { get => _price; }

		public decimal GetCost() =>
			_price * Tariff.GetMultiplier();

		public override string? ToString()
		{
			return $"\t{_name} \t{_tariff}";
		}
	}
}
