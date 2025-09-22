using LR6.Domain;
using LR6.Mediator;
using System.Text.Json;

namespace LR6.UseCases
{
	/// <summary>
	/// Запрос на сохранение коллекции в файл
	/// </summary>
	/// <param name="data">Коллекция объектов</param>
	/// <param name="fileName">имя файла</param>
	internal sealed record SaveData(IEnumerable<ToDoTask> data, string fileName) : IRequest;

	/// <summary>
	/// Обработчик запроса
	/// </summary>
	internal class SaveDataHandler : IRequestHandler<SaveData>
	{
		public void Handle(SaveData request)
		{
			using (var fs = new FileStream($"{request.fileName}.json", FileMode.OpenOrCreate))
			{
				JsonSerializer.Serialize(fs, request.data);
			}
		}
	}
}
