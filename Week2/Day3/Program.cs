using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Sequential execution
        Console.WriteLine("Sequential Execution:");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        await GetDataFromSource1();
        await GetDataFromSource2();
        await GetDataFromSource3();

        stopwatch.Stop();
        Console.WriteLine($"Time Taken: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("-----------------------------");

        // Concurrent execution
        Console.WriteLine("Concurrent Execution:");

        stopwatch.Restart();

        Task task1 = GetDataFromSource1();
        Task task2 = GetDataFromSource2();
        Task task3 = GetDataFromSource3();

        await Task.WhenAll(task1, task2, task3);

        stopwatch.Stop();
        Console.WriteLine($"Time Taken: {stopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("-----------------------------");

        // Cancellation Token example
        CancellationTokenSource cts = new CancellationTokenSource();

        Task longTask = LongOperation(cts.Token);

        cts.CancelAfter(2000);

        try
        {
            await longTask;
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Operation was cancelled.");
        }
    }

    static async Task GetDataFromSource1()
    {
        await Task.Delay(1000);
        Console.WriteLine("Source 1 finished");
    }

    static async Task GetDataFromSource2()
    {
        await Task.Delay(1500);
        Console.WriteLine("Source 2 finished");
    }

    static async Task GetDataFromSource3()
    {
        await Task.Delay(2000);
        Console.WriteLine("Source 3 finished");
    }

    static async Task LongOperation(CancellationToken token)
    {
        Console.WriteLine("Long operation started...");

        await Task.Delay(5000, token);

        Console.WriteLine("Long operation finished.");
    }
}