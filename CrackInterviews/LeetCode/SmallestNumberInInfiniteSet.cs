namespace LeetCode;

public class SmallestInfiniteSet
{
    private readonly PriorityQueue<int, int> _addedBack;
    private readonly ISet<int> _addedBackSet;
    private int _count;

    public SmallestInfiniteSet()
    {
        _count = 1;
        _addedBack = new PriorityQueue<int, int>();
        _addedBackSet = new HashSet<int>();
    }

    public int PopSmallest()
    {
        if (_addedBack.TryPeek(out var element, out _))
        {
            if (element < _count)
            {
                _addedBackSet.Remove(element);
                return _addedBack.Dequeue();
            }
        }

        return _count++;
    }

    public void AddBack(int num)
    {
        if (num < _count)
        {
            if (!_addedBackSet.Contains(num))
            {
                _addedBackSet.Add(num);
                _addedBack.Enqueue(num, num);
            }
        }
    }
}