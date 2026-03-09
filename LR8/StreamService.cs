using System.Text.Json;

class StreamService<T>
{
    public async Task WriteToStreamAsync(Stream stream, IEnumerable<T> data, IProgress<(int, string)> progress)
    {
        progress.Report((Thread.CurrentThread.ManagedThreadId, "Begin of writing"));
        await JsonSerializer.SerializeAsync(stream, data);
        await Task.Delay(3000);
        progress.Report((Thread.CurrentThread.ManagedThreadId, "End of writing"));
    }

    public async Task CopyFromStreamAsync(Stream stream, string filename, IProgress<(int, string)> progress)
    {
        progress.Report((Thread.CurrentThread.ManagedThreadId, "Begin of copying"));
        stream.Seek(0, SeekOrigin.Begin);
        using var fs = File.Create(filename);
        await stream.CopyToAsync(fs);
        progress.Report((Thread.CurrentThread.ManagedThreadId, "End of copying"));
    }

    public async Task<int> GetStatisticsAsync(string fileName, Func<T, bool> filter)
    {
        using var fs = File.Open(fileName, FileMode.Open, FileAccess.Read);
        var data = await JsonSerializer.DeserializeAsync<List<T>>(fs);
        if (data == null)
        {
            return 0;
        }
        return data.Where(filter).Count();
    }
}
