namespace LR3.Contracts
{
    interface IMaintenanceService
    {
        void AddResident(string resident_name);
        public void AddService(string service_name, decimal service_price, decimal service_time);
		void AddServiceToResident(string resident_name, string service_name);
        decimal TotalCost();
        decimal ResidentCost(string resident_name);
        int ServiceCount(string service_name);
	}
}
