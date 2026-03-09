using System.Diagnostics;

class IntegralSolver
{
    public delegate void ElapsedHandler(int thread_id, double result, long ticks);
    public event ElapsedHandler? ElapsedNotify;

    public delegate void ProgressHandler(int thread_id, int progress);
    public event ProgressHandler? ProgressNotify;

    private static Semaphore semaphore = new(3, 3);

    public void Solve()
    {
        semaphore.WaitOne();

        var stopwatch = new Stopwatch();
        var thread_id = Thread.CurrentThread.ManagedThreadId;

        stopwatch.Start();
        double a = 0.0;
        double b = 1.0;
        double step = 0.00000001;
        double result = 0;
        for (double i = a; i < b; i += step)
        {
            result += Math.Sin(i + step / 2) * step;
            for (int j = 0; j < 100; j++)
            {
                result *= 1;
            }

            int progress = (int) Math.Round((i - step) / (b - a) * 100);
            if (progress > Math.Round((i - 2 * step) / (b - a) * 100))
            {
                ProgressNotify?.Invoke(thread_id, progress);
            }
        }
        stopwatch.Stop();

        ElapsedNotify?.Invoke(thread_id, result, stopwatch.ElapsedTicks);

        semaphore.Release();
    }
}