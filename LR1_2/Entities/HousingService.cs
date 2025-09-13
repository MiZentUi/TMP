using LR1_2.Collections;
using LR1_2.Contracts;
using LR1_2.Interfaces;
using System.Xml.Linq;

namespace LR1_2.Entities
{
	class HousingService : IMaintenanceService
	{
		private ICustomCollection<Resident> _residents;
		private ICustomCollection<Service> _services;

		public delegate void ChangeHandler(string message);
		public event ChangeHandler? ChangeNotify;
		public delegate void ResidentHandler(string message);
		public event ResidentHandler? ResidentNotify;

		public HousingService()
		{
			_residents = new CustomCollection<Resident>();
			_services = new CustomCollection<Service>();
		}

		public void AddResident(string resident_name)
		{
			_residents.Add(new Resident(resident_name));
			ChangeNotify?.Invoke($"HousingService: New resident with name {resident_name} has been added");
		}

		public void AddService(string service_name, decimal service_price, TariffType tariffType)
		{
			_services.Add(new Service(service_name, service_price, new Tariff(tariffType)));
		}

		public void AddServiceToResident(string resident_name, string service_name)
		{
			Resident? resident = null;
			foreach (var current_resident in _residents)
				if (current_resident.Name == resident_name)
					resident = current_resident;
			Service? service = null;
			foreach (var current_service in _services)
				if (current_service.Name == service_name)
					service = current_service;
			if (resident != null && service != null)
			{
				resident.AddService(service);
				ResidentNotify?.Invoke($"Resident {{{resident.Name}}}: New service with name {service.Name} has been added");
			}
		}

		public decimal TotalCost()
		{
			decimal cost = 0;
			foreach (var resident in _residents)
				cost += resident.GetCost();
			return cost;
		}

		public decimal ResidentCost(string resident_name)
		{
			foreach (var resident in _residents)
				if (resident_name == resident.Name)
					return resident.GetCost();
			return 0;
		}

		public int ServiceCount(string service_name)
		{
			int counter = 0;
			foreach (var resident in _residents)
				foreach (var currentService in resident.Services)
					if (service_name.ToLower() == currentService.Name.ToLower())
						counter++;
			return counter;
		}

		public override string? ToString()
		{
			string result = "HousingService: \n";
			foreach (var resident in _residents)
				result += resident + "\n";
			return result;
		}
	}
}
