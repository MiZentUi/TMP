using LR3.Contracts;

namespace LR3.Entities
{
	class HousingService : IMaintenanceService
	{
		private List<Resident> _residents;
		private Dictionary<string, Service> _services;

		public delegate void ChangeHandler(string message);
		public event ChangeHandler? ChangeNotify;
		public delegate void ResidentHandler(string message);
		public event ResidentHandler? ResidentNotify;

		public HousingService()
		{
			_residents = [];
			_services = [];
		}

		public void AddResident(string resident_name)
		{
			_residents.Add(new Resident(resident_name));
			ChangeNotify?.Invoke($"HousingService: New resident with name {resident_name} has been added");
		}

		public void AddService(string service_name, decimal service_price, decimal service_time)
		{
			_services.Add(service_name, new Service(service_price, service_time));
		}

		public void AddServiceToResident(string resident_name, string service_name)
		{
			Resident? resident = (from r in _residents where r.Name == resident_name select r).First();
			if (resident != null && _services.ContainsKey(service_name))
			{
				resident.AddService(_services[service_name]);
				ResidentNotify?.Invoke($"Resident {{{resident.Name}}}: New service with name {service_name} has been added");
			}
		}

		public decimal TotalCost() =>
			(from r in _residents select r.GetCost()).Sum();

		public decimal ResidentCost(string resident_name) =>
			(from r in _residents where r.Name == resident_name select r).First().GetCost();

		public int ServiceCount(string service_name) =>
			_services.Where(s => s.Key == service_name).Count();

		public IEnumerable<string> SortedServiceNames() =>
			_services.OrderBy(s => s.Value.GetCost()).Select(s => s.Key);

		public string RichestResident() =>
			_residents.OrderByDescending(r => r.GetCost()).First().Name;

		public int ResidentsCountGreaterThan(decimal cost) =>
			_residents.Aggregate(0, (count, r) => r.GetCost() > cost ? ++count : count);

		public override string? ToString()
		{
			string result = "HousingService: \n";
			foreach (var resident in _residents)
				result += resident + "\n";
			return result;
		}
	}
}
