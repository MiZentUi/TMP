namespace LR3.Entities
{
	internal class Resident(string name)
	{
		private List<Service> _services = [];

		public string Name { get => name; }
		public List<Service> Services { get => _services; }

		public void AddService(Service service) =>
			_services.Add(service);

		public decimal GetCost() =>
			(from s in _services select s.GetCost()).Sum();

		public override string? ToString()
		{
			string result = $"{name}\n";
			foreach (var service in _services)
				result += "\t" + service + "\n";
			result += $"\tCoast: {GetCost()}";
			return result;
		}
	}
}
