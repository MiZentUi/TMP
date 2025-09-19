using System.Text.Json;

namespace LR4
{
	class FileService<T> : IFileService<T>
	{
		public IEnumerable<T> ReadFile(string fileName)
		{
			using (BinaryReader br = new(new FileStream(fileName, FileMode.OpenOrCreate)))
			{
				IEnumerable<T> l;
				try
				{
					l = JsonSerializer.Deserialize<IEnumerable<T>>(br.ReadString())!;
				}
				catch (Exception e) when (e is IOException || e is ObjectDisposedException)
				{
					Console.WriteLine(e.Message);
					yield break;
				}
				if (l != null)
				{
					foreach (var i in l)
					{
						yield return i;
					}
				}
			}
		}

		public void SaveData(IEnumerable<T> data, string fileName)
		{
			using (BinaryWriter bw = new(new FileStream(fileName, FileMode.OpenOrCreate)))
			{
				try
				{
					bw.Write(JsonSerializer.Serialize(data));
				}
				catch (Exception e) when (e is IOException || e is ObjectDisposedException)
				{
					Console.WriteLine(e.Message);
				}
			}
		}
	}
}
