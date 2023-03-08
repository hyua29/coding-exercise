using System.Collections.Concurrent;

namespace DotnetStateExperiments;

public class ThreadPoolDispatcher
{
    private ConcurrentBag<Task> _tasks;

    private Thread _TaskInspector;

    private ManualResetEventSlim _dispatcherResetEvent;

    private AutoResetEvent _hasWork;

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
        {
            if (_hasWork.WaitOne())
            {
                // var completedTask = _tasks.
            }
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