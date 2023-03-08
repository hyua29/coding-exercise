using System.Collections;

namespace C7;

public class CircularArray<T> : IEnumerable<T>
{
    private T[] _innerArray;

    private int _pointer;

    public CircularArray(int size)
    {
        _innerArray = new T[size];
    }

    public T this[int index]
    {
        get => _innerArray[index];
        set => _innerArray[index] = value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new CircularArrayIEnumerator<T>(_innerArray);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private class CircularArrayIEnumerator<T> : IEnumerator<T>
    {
        private readonly T[] _array;

        private int _pointer;

        public CircularArrayIEnumerator(T[] array)
        {
            _array = array;
        }

        public bool MoveNext()
        {
            return _pointer < _array.Length;
        }

        public void Reset()
        {
            _pointer = 0;
        }

        public T Current => _array[_pointer++];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}



[TestFixture]
public class BuildOrderTests
{
    [Test]
    public void Test()
    {
        var circularArray = new CircularArray<int>(10);
        circularArray[0] = 0;
        circularArray[1] = 1;
        circularArray[2] = 2;
        circularArray[3] = 3;
        circularArray[4] = 4;
        circularArray[5] = 5;
        circularArray[6] = 6;
        circularArray[7] = 7;
        circularArray[8] = 8;
        circularArray[9] = 9;

        foreach (var v in circularArray)
        {
            Console.WriteLine(v);
        }
    }
}