namespace LR3.Entities
{
    class Journal
    {
        private List<string> _messages;

        public Journal()
		{
			 _messages = [];
		}

		public void LogEvent(string message) =>
            _messages.Add(message);

        public void Print()
        {
            foreach (var message in _messages)
				Console.WriteLine($"[Journal] {message}");
        }
    }
}
