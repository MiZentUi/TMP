using LR1_2.Collections;
using LR1_2.Entities;
using LR1_2.Interfaces;

var journal = new Journal();

var housingService = new HousingService();
housingService.ChangeNotify += journal.LogEvent;
housingService.ResidentNotify += (string message) => { Console.WriteLine(message); };

housingService.AddService("Heating", 5, TariffType.Standart);
housingService.AddService("Electricity", 10, TariffType.Preferential);
housingService.AddService("Water Supply", 10, TariffType.Standart);
housingService.AddService("Maintenance", 20, TariffType.Special);

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
Console.WriteLine();

journal.Print();
Console.WriteLine();

Console.WriteLine("Collection: ");
ICustomCollection<int> collection = new CustomCollection<int>();
for (int i = 0; i < 5; i++)
	collection.Add(i + 1);
foreach (var i in collection)
	Console.WriteLine(i);
collection.Remove(6);