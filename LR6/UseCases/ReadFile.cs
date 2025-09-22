using LR6.Domain;
using LR6.Mediator;
using System.Text.Json;

namespace LR6.UseCases
{
	/// <summary>
	/// Запрос на чтение файла
	/// </summary>
	/// <param name="FileName">Имя файла</param>
	internal sealed record ReadFile(string FileName) : IRequest<IEnumerable<ToDoTask>>;

	/// <summary>
	/// Обработчик запроса чтения файла
	/// </summary>
	internal class ReadFileHandler : IRequestHandler<ReadFile, IEnumerable<ToDoTask>>
	{
		public IEnumerable<ToDoTask> Handle(ReadFile request)
		{
			using (var fs = new FileStream($"{request.FileName}.json", FileMode.OpenOrCreate))
			{
				var tasks = JsonSerializer.Deserialize<List<ToDoTask>>(fs);
				if (tasks != null)
				{
					return tasks;
				}
			}
			return new List<ToDoTask>();
		}
	}
}
