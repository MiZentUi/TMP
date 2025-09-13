using LR3.Entities;

var journal = new Journal();

var housingService = new HousingService();
housingService.ChangeNotify += journal.LogEvent;
housingService.ResidentNotify += (string message) => { Console.WriteLine(message); };

housingService.AddService("Heating", 2, 24);
housingService.AddService("Electricity", 3, 12);
housingService.AddService("Water Supply", 2, 24);
housingService.AddService("Maintenance", 10, 6);

housingService.AddResident("Green");
housingService.AddResident("Smith");
housingService.AddResident("Brown");
housingService.AddResident("White");
housingService.AddResident("Pinkman");

housingService.AddServiceToResident("Green", "Heating");
housingService.AddServiceToResident("Green", "Electricity");
housingService.AddServiceToResident("Green", "Water Supply");
housingService.AddServiceToResident("Green", "Maintenance");

housingService.AddServiceToResident("Smith", "Heating");
housingService.AddServiceToResident("Smith", "Electricity");
housingService.AddServiceToResident("Smith", "Water Supply");

housingService.AddServiceToResident("Brown", "Heating");
housingService.AddServiceToResident("Brown", "Electricity");
housingService.AddServiceToResident("Brown", "Water Supply");
housingService.AddServiceToResident("Brown", "Maintenance");

housingService.AddServiceToResident("White", "Heating");
housingService.AddServiceToResident("White", "Electricity");
housingService.AddServiceToResident("White", "Water Supply");

housingService.AddServiceToResident("Pinkman", "Heating");
housingService.AddServiceToResident("Pinkman", "Electricity");
housingService.AddServiceToResident("Pinkman", "Water Supply");
housingService.AddServiceToResident("Pinkman", "Maintenance");

Console.WriteLine();
Console.WriteLine(housingService);
Console.WriteLine($"White Cost: {housingService.ResidentCost("White")}");
Console.WriteLine($"Total Cost: {housingService.TotalCost()}");
Console.WriteLine($"Heating Service Count: {housingService.ServiceCount("Heating")}");
Console.Write($"Sorted Service Names: [");
foreach (var service_name in housingService.SortedServiceNames())
	Console.Write(service_name + " ");
Console.WriteLine("]");
Console.WriteLine($"Richest Resident: {housingService.RichestResident()}");
Console.WriteLine($"Residents Count Greater Than 135: {housingService.ResidentsCountGreaterThan(135)}");
Console.WriteLine();

journal.Print();
