namespace DotnetStateExperiments;

using System.Collections.Concurrent;

public class ThreadPoolDispatcher
{
    private readonly ManualResetEventSlim _dispatcherResetEvent;

    private readonly AutoResetEvent _hasWork;

    private readonly Thread _TaskInspector;
    private readonly ConcurrentBag<Task> _tasks;

    public ThreadPoolDispatcher()
    {
        _tasks = new ConcurrentBag<Task>();
        _TaskInspector = new Thread(InspectTasks);
        _dispatcherResetEvent = new ManualResetEventSlim(true);
        _hasWork = new AutoResetEvent(false);
    }

    private void InspectTasks()
    {
        // Run the loop until the dispatcher is shut down
        while (_dispatcherResetEvent.IsSet)
            if (_hasWork.WaitOne())
            {
                // var completedTask = _tasks.
            }
    }

    public void Start()
    {
        _TaskInspector.Start();
    }

    public void Stop()
    {
        _dispatcherResetEvent.Reset();
    }

    public void DispatchTask(Func<Task> func)
    {
        _tasks.Add(func());
        _hasWork.Set();
    }
}