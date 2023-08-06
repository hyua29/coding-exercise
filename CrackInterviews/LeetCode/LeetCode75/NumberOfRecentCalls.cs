namespace LeetCode.LeetCode75;

public class RecentCounter
{
    private readonly Queue<int> _queue;

    public RecentCounter()
    {
        _queue = new Queue<int>();
    }

    public int Ping(int t)
    {
        _queue.Enqueue(t);

        while (_queue.TryPeek(out var v) && t - v > 3000)
        {
            _queue.Dequeue();
        }

        return _queue.Count;
    }
}