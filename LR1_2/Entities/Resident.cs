using LR1_2.Interfaces;
using LR1_2.Collections;

namespace LR1_2.Entities
{
	internal class Resident(string name)
	{
		private ICustomCollection<Service> _services = new CustomCollection<Service>();

		public string Name { get => name; }
		public ICustomCollection<Service> Services { get => _services; }

		public void AddService(Service service)
		{
			_services.Add(service);
		}

		public decimal GetCost()
		{
			decimal cost = 0;
			foreach (var service in _services)
			{
				cost += service.GetCost();
			}
			return cost;
		}

		public override string? ToString()
		{
			string result = $"{name}\n";
			foreach (var service in _services)
				result += service + "\n";
			result += $"\tCoast: {GetCost()}";
			return result;
		}
	}
}
