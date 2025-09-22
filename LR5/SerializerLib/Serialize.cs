using LR5.Domain;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace LR5.SerializerLib
{
	internal class Serialize : ISerializer
	{
		public IEnumerable<RailwayStation> DeSerializeByLINQ(string fileName)
		{
			var xDocument = XDocument.Load($"{fileName}.linq.xml");
			var xStations = xDocument.Element("RailwayStations");
			if (xStations != null)
			{
				return xStations.Elements("RailwayStation").Select(station => new RailwayStation(
					station.Element("Name")?.Value!,
					station.Element("City")?.Value!,
					new LuggageCompartment(
						int.Parse(station.Element("LuggageCompartment")?.Element("Id")?.Value!),
						int.Parse(station.Element("LuggageCompartment")?.Element("Capacity")?.Value!),
						decimal.Parse(station.Element("LuggageCompartment")?.Element("Price")?.Value!)
					)
				)).ToList();
			}
			return new List<RailwayStation>();
		}

		public IEnumerable<RailwayStation> DeSerializeJSON(string fileName)
		{
			using (var fs = new FileStream($"{fileName}.json", FileMode.OpenOrCreate))
			{
				var stations = JsonSerializer.Deserialize<List<RailwayStation>>(fs);
				if (stations != null)
				{
					return stations;
				}
			}
			return new List<RailwayStation>();
		}

		public IEnumerable<RailwayStation> DeSerializeXML(string fileName)
		{
			var xmlSerializer = new XmlSerializer(typeof(List<RailwayStation>));
			using (var fs = new FileStream($"{fileName}.xml", FileMode.OpenOrCreate))
			{
				var stations = xmlSerializer.Deserialize(fs) as IEnumerable<RailwayStation>;
				if (stations != null)
				{
					return stations;
				}
			}
			return new List<RailwayStation>();
		}

		public void SerializeByLINQ(IEnumerable<RailwayStation> stations, string fileName)
		{
			var xDocument = new XDocument(new XElement("RailwayStations",
					stations.Select(s => new XElement("RailwayStation",
						new XElement("Name", s.Name),
						new XElement("City", s.City),
						new XElement("LuggageCompartment",
							new XElement("Id", s.LuggageCompartment.Id),
							new XElement("Capacity", s.LuggageCompartment.Capacity),
							new XElement("Price", s.LuggageCompartment.Price)
						)
					))
				)
			);
			xDocument.Save($"{fileName}.linq.xml");
		}

		public void SerializeJSON(IEnumerable<RailwayStation> stations, string fileName)
		{
			using (var fs = new FileStream($"{fileName}.json", FileMode.OpenOrCreate))
			{
				JsonSerializer.Serialize(fs, stations);
			}
		}

		public void SerializeXML(IEnumerable<RailwayStation> stations, string fileName)
		{
			var xmlSerializer = new XmlSerializer(typeof(List<RailwayStation>));
			using (var fs = new FileStream($"{fileName}.xml", FileMode.OpenOrCreate))
			{
				xmlSerializer.Serialize(fs, stations);
			}
		}
	}
}
