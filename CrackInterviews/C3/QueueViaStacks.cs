namespace C3
{
    using System.Collections.Generic;

    public class QueueViaStacks<T>
    {
        private Stack<T> _stackNewest;
        private Stack<T> _stackOldest;

        public QueueViaStacks()
        {
            _stackNewest = new Stack<T>();
            _stackOldest = new Stack<T>();
        }

        public int Count => _stackNewest.Count + _stackOldest.Count;

        public void Push(T item)
        {
            _stackNewest.Push(item);
        }

        public T Pop()
        {
            if (Count <= 0)
            {
                return default(T);
            }

            if (_stackOldest.TryPop(out T result))
            {
                return result;
            }

            while (_stackNewest.TryPop(out T item))
            {
                _stackOldest.Push(item);
            }

            return _stackOldest.Pop();
        }
    }
}