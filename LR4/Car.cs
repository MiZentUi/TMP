namespace LR4
{
    class Car(string name, int capacity) : INamed
    {
        

        public string Name { get => name; }
        public int Capacity { get => capacity; }

		public override string? ToString() =>
			$"Car: {{name: {name}, capacity: {capacity}}}";
	}
}
