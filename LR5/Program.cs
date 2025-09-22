using LR5.Domain;
using LR5.SerializerLib;
using Microsoft.Extensions.Configuration;

var fileName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()["file_name"]!;
Console.WriteLine($"File name: {fileName}");

var stations = new List<RailwayStation>
{
	new("Minsk Passanger", "Minsk", new LuggageCompartment(1, 100, 2.5M)),
	new("Kobrin Station", "Kobrin", new LuggageCompartment(2, 20, 1.5M)),
	new("Svetlogorsk na Berezine", "Svetlogorsk", new LuggageCompartment(3, 30, 2M)),
	new("Zhlobin Station", "Zhlobin", new LuggageCompartment(4, 20, 1.5M)),
	new("Molodechno Station", "Molodechno", new LuggageCompartment(5, 20, 1.5M))
};

Console.WriteLine("Staions: ");
foreach (var station in stations)
{
	Console.WriteLine(station);
}

var serializer = new Serialize();
serializer.SerializeByLINQ(stations, fileName);
serializer.SerializeXML(stations, fileName);
serializer.SerializeJSON(stations, fileName);

Console.WriteLine("LINQ stations: ");
var linqStations = serializer.DeSerializeByLINQ(fileName).ToList();
for (int i = 0; i < linqStations.Count; i++)
{
	Console.WriteLine($"Is equals: {linqStations[i].Equals(stations[i])} {linqStations[i]}");
}

Console.WriteLine("XML stations: ");
var xmlStations = serializer.DeSerializeXML(fileName).ToList();
for (int i = 0; i < xmlStations.Count; i++)
{
	Console.WriteLine($"Is equals: {xmlStations[i].Equals(stations[i])} {xmlStations[i]}");
}

Console.WriteLine("JSON stations: ");
var jsonStations = serializer.DeSerializeJSON(fileName).ToList();
for (int i = 0; i < jsonStations.Count; i++)
{
	Console.WriteLine($"Is equals: {jsonStations[i].Equals(stations[i])} {jsonStations[i]}");
}