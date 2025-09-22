namespace LR5.Domain
{
	[Serializable]
	public class LuggageCompartment : IEquatable<LuggageCompartment>
	{
		public int Id { get; set; } = 0;
		public int Capacity { get; set; } = 0;
		public decimal Price { get; set; } = 0;

		public LuggageCompartment() { }

		public LuggageCompartment(int id, int capacity, decimal price)
		{
			Id = id;
			Capacity = capacity;
			Price = price;
		}

		public bool Equals(LuggageCompartment? other) =>
			other != null && Id.Equals(other.Id) && Capacity.Equals(other.Capacity) && Price.Equals(other.Price);

		public override string? ToString() => 
			$"LuggageCompartment{{id: {Id}, capacity: {Capacity}, price: {Price}}}";
	}
}
