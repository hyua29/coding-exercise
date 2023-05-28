namespace DotnetStateExperiments;

public class TaskTests
{
    [Test]
    public async Task ThreadTest()
    {
        var c = new HttpClient(new HttpClientHandler());
        var t = new Thread(() =>
        {
            var s = SynchronizationContext.Current;
            Console.WriteLine($"Test starting, current thread id - {Thread.CurrentThread.ManagedThreadId}");
        });


        t.Start();

        t.Join();
    }

    [Test]
    public async Task Test()
    {
        Console.WriteLine($"Test starting, current thread id - {Thread.CurrentThread.ManagedThreadId}");

        var waitTask = WaitAsync();

        Console.WriteLine($"Doing some other work, current thread id - {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(500);
        Console.WriteLine($"Finishing some other work, current thread id - {Thread.CurrentThread.ManagedThreadId}");
        await waitTask;
    }

    private async Task WaitAsync()
    {
        Console.WriteLine($"Wait task started, current thread id - {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(100);
        Console.WriteLine($"Wait task finishing, current thread id - {Thread.CurrentThread.ManagedThreadId}");
    }
}