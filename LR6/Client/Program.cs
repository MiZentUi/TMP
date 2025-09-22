using LR6.Domain;
using LR6.Mediator;
using System.Reflection;

var fileName = "tasks";

var sender = new Sender();

var assembly = Assembly.GetExecutingAssembly();

var saveDataConstructor = assembly.GetType("LR6.UseCases.SaveData")?.GetConstructor([typeof(IEnumerable<ToDoTask>), typeof(string)]);
var readFileConstructor = assembly.GetType("LR6.UseCases.ReadFile")?.GetConstructor([typeof(string)]);

var tasks = new List<ToDoTask>()
{
	new("Create request handler", false),
	new("Add .gitignore", true),
	new("Create Pull Request", true),
};

if (saveDataConstructor != null)
{
	sender.Send((IRequest)saveDataConstructor.Invoke([tasks, fileName]));
}

Console.WriteLine("File ToDoTasks: ");
foreach (var task in tasks)
{
	Console.WriteLine($"\t{task}");
}
Console.WriteLine();

if (readFileConstructor != null)
{
	var fileTasks = sender.Send((IRequest<IEnumerable<ToDoTask>>)readFileConstructor.Invoke([fileName])).ToList();
	Console.WriteLine("ToDoTasks: ");
	for (int i = 0; i < fileTasks.Count; i++)
	{
		Console.WriteLine($"\tIs equals: {fileTasks[i].Equals(tasks[i])} {fileTasks[i]}");
	}
}