using LR1_2.Collections;
using LR1_2.Interfaces;

namespace LR1_2.Entities
{
    class Journal
    {
        private ICustomCollection<string> _messages;

        public Journal()
		{
			 _messages = new CustomCollection<string>();
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
