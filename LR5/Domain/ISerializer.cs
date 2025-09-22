namespace LR5.Domain
{
	internal interface ISerializer
	{
		IEnumerable<RailwayStation> DeSerializeByLINQ(string fileName);
		IEnumerable<RailwayStation> DeSerializeXML(string fileName);
		IEnumerable<RailwayStation> DeSerializeJSON(string fileName);
		void SerializeByLINQ(IEnumerable<RailwayStation> stations, string fileName);
		void SerializeXML(IEnumerable<RailwayStation> stations, string fileName);
		void SerializeJSON(IEnumerable<RailwayStation> stations, string fileName);
	}
}
