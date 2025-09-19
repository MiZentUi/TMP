using LR4;

internal class Program
{
	private	static string filesPath = "files";

	private static void Main(string[] args)
	{
		Directory.CreateDirectory(filesPath);

		CreateFiles(10);
		Console.WriteLine("Files");
		PrintFiles();
		Console.WriteLine();

		var firstFile = Path.Combine(filesPath, "cars_1.json");
		var secondFile = Path.Combine(filesPath, "cars_2.json");
		var fileService = new FileService<Car>();
		var cars1 = CreateCarsList(5);
		fileService.SaveData(cars1, firstFile);
		File.Move(firstFile, secondFile, true);
		var cars2 = new List<Car>(fileService.ReadFile(secondFile));
		Console.WriteLine("Cars 1");
		foreach (var car in cars1)
			Console.WriteLine(car);
		Console.WriteLine();
		Console.WriteLine("Cars 2");
		foreach (var car in cars2.OrderBy(car => car, new CustomComparer<Car>()))
			Console.WriteLine(car);
		Console.WriteLine();
		Console.WriteLine("Cars 3");
		foreach (var car in cars1.OrderBy(car => car.Capacity))
			Console.WriteLine(car);
		Console.WriteLine();
	}

	private static void CreateFiles(int count)
	{
		for (int i = 0; i < count; i++)
			File.Open(Path.Combine(filesPath, Path.ChangeExtension(Path.GetRandomFileName(), new[] { ".txt", ".rtf", ".dat", ".inf" }[new Random((int)DateTime.Now.Ticks).Next(4)])), FileMode.Create);
	}

	private static void PrintFiles()
	{
		foreach (var file in Directory.GetFiles(filesPath))
			Console.WriteLine($"File: {Path.GetFileName(file)} has extension {Path.GetExtension(file)}");
	}

	private static List<Car> CreateCarsList(int count)
	{
		List<string> carTypes = ["Sport", "Sedan", "Crossover", "Minivan", "Hatchback", "Passat"];
		var random = new Random((int)DateTime.Now.Ticks);
		List<Car> cars = [];
		for (int i = 0; i < count; i++)
			cars.Add(new Car(carTypes[Random.Shared.Next(carTypes.Count)], random.Next(2, 6)));
		return cars;
	}
}