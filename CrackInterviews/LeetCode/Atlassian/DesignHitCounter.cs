namespace LeetCode.Atlassian;

public class HitCounter
{
    private readonly Queue<int> _slidingWindow;

    private const int WindowSize = 300;

    public HitCounter()
    {
        _slidingWindow = new Queue<int>();
    }

    public void Hit(int timestamp)
    {
        RemoveOutdatedData(timestamp);

        _slidingWindow.Enqueue(timestamp);
    }

    public int GetHits(int timestamp)
    {
        RemoveOutdatedData(timestamp);

        return _slidingWindow.Count;
    }

    private void RemoveOutdatedData(int timestamp)
    {
        while (_slidingWindow.TryPeek(out var t) && timestamp - WindowSize >= t)
        {
            _slidingWindow.Dequeue();
        }
    }
}