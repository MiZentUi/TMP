using LR1_2.Entities;

namespace LR1_2.Contracts
{
    interface IMaintenanceService
    {
        void AddResident(string resident_name);
        void AddService(string service_name, decimal service_price, TariffType tariffType);
        void AddServiceToResident(string resident_name, string service_name);
        decimal TotalCost();
        decimal ResidentCost(string resident_name);
        int ServiceCount(string service_name);
	}
}
