Console.Clear();

var locker = new Lock();
var solver = new IntegralSolver();

solver.ProgressNotify += (id, progress) =>
{
    lock (locker)
    {
        var prompt = $"Thread {id,2}: [{new string('#', progress)}{new string(' ', 100 - progress)}] {progress,3}%\r";
        Console.SetCursorPosition(0, id % 5);
        Console.Write(prompt);
    }
};

solver.ElapsedNotify += (id, result, ticks) =>
{
    lock (locker)
    {
        Console.SetCursorPosition(0, id % 5);
        Console.Write(new string(' ', Console.WindowWidth) + '\r');
        Console.WriteLine($"Thread {id,2}: Complete with result: {result,0:F5} - Elapsed ticks: {ticks}");
        Console.SetCursorPosition(0, 5);
    }
};

var thread_1 = new Thread(solver.Solve);
var thread_2 = new Thread(solver.Solve);
var thread_3 = new Thread(solver.Solve);
var thread_4 = new Thread(solver.Solve);
var thread_5 = new Thread(solver.Solve);

thread_1.Priority = ThreadPriority.Highest;
thread_2.Priority = ThreadPriority.Lowest;

thread_1.Start();
thread_2.Start();
thread_3.Start();
thread_4.Start();
thread_5.Start();
