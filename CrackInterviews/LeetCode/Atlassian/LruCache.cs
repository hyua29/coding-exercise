using System.Diagnostics;

namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/lru-cache/
/// </summary>
public class LruCache
{
    private readonly int _capacity;
    private readonly IDictionary<int, LinkedListNode<(int Key, int Value)>> _queueCache;
    private readonly LinkedList<(int Key, int Value)> _queue;

    public LruCache(int capacity)
    {
        _capacity = capacity;
        _queue = new LinkedList<(int Key, int Value)>();
        _queueCache = new Dictionary<int, LinkedListNode<(int Key, int Value)>>(capacity);
    }

    public int Get(int key)
    {
        if (_queueCache.TryGetValue(key, out var value))
        {
            UpdateQueue(key);
            return value.Value.Value;
        }

        return -1;
    }


    public void Put(int key, int value)
    {
        if (_queueCache.ContainsKey(key))
        {
            UpdateQueue(key, value);
        }
        else
        {
            if (_queue.Count == _capacity)
            {
                _queueCache.Remove(_queue.First!.Value.Key);
                _queue.RemoveFirst();
            }

            var node = new LinkedListNode<(int, int)>((key, value));
            _queueCache.Add(key, node);
            _queue.AddLast(node);
        }

        // Debug.Assert(_queue.Count == _queueCache.Count);
    }

    private void UpdateQueue(int key, int? value = null)
    {
        var node = _queueCache[key];

        if (value != null) node.Value = (key, value.Value);

        _queue.Remove(node);
        _queue.AddLast(node);
    }
}