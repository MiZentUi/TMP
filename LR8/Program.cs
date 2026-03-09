int thread_id = Thread.CurrentThread.ManagedThreadId;

var progress = new Progress<(int, string)>(item => Console.WriteLine($"Thread {item.Item1}: {item.Item2}"));

var profiles = new List<JobProfile>();

var random = new Random();
var words = LoremNET.Lorem.Words(5).Split();

for (int i = 0; i < 1000; i++)
{
    profiles.Add(new JobProfile(i, words[random.Next(words.Length)], random.Next(1, 1000)));
}

Console.WriteLine($"Thread {thread_id}: Getting started");

var streamService = new StreamService<JobProfile>();
var stream = new MemoryStream();
var filename = "temp.json";

var task_1 = streamService.WriteToStreamAsync(stream, profiles, progress);
await Task.Delay(200);
var task_2 = task_1.ContinueWith((_) => streamService.CopyFromStreamAsync(stream, filename, progress));

Console.WriteLine($"Thread {thread_id}: Tasks are started");

await Task.WhenAll(task_1, task_2);

var current_word = words[random.Next(words.Length)];
var result = await streamService.GetStatisticsAsync(filename, profile => profile.Name.Equals(current_word));

Console.WriteLine($"Profile: {current_word} - result: {result}");
