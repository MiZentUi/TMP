namespace LR5.Domain
{
	[Serializable]
	public class RailwayStation : IEquatable<RailwayStation>
	{
		public string Name { get; set; }
		public string City { get; set; }
		public LuggageCompartment LuggageCompartment { get; set; }

		public RailwayStation() : this("", "", new LuggageCompartment()) {}

		public RailwayStation(string name, string city, LuggageCompartment luggageCompartment)
		{
			Name = name;
			City = city;
			LuggageCompartment = luggageCompartment;
		}

		public bool Equals(RailwayStation? other) =>
			other != null && Name.Equals(other.Name) && City.Equals(other.City) && LuggageCompartment.Equals(other.LuggageCompartment);

		public override string? ToString() => 
			$"RailwayStation{{name: {Name}, city: {City}, language_compartment: {LuggageCompartment}}}";
	}
}
