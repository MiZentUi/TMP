namespace LR3.Entities
{
	internal class Service(decimal _price, decimal _time = 1)
	{
		public decimal Price { get => _price; }
		public decimal Time { get => _time; }

		public decimal GetCost() =>
			_price * _time;

		public override string? ToString()
		{
			return $"Service: {{{_price}, {_time}}}";
		}
	}
}
