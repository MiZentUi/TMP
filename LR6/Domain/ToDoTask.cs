namespace LR6.Domain
{
	internal class ToDoTask(string name, bool completed) : IEquatable<ToDoTask>
	{
		public string Name { get => name; }
		public bool Completed { get => completed; }

		public bool Equals(ToDoTask? other) =>
			other != null && name.Equals(other.Name) && completed.Equals(other.Completed);

		public override string? ToString() =>
			$"ToDoTask{{name: {name}, complete: {completed}}}";
	}
}
