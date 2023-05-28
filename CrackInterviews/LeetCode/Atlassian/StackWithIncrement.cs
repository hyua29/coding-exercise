namespace LeetCode.Atlassian;

public class CustomStack
{
    private readonly int[] _inc;
    private readonly int _n;
    private readonly Stack<int> _stack;

    public CustomStack(int maxSize)
    {
        _n = maxSize;
        _inc = new int[_n];
        _stack = new Stack<int>();
    }

    public void Push(int x)
    {
        if (_stack.Count < _n)
            _stack.Push(x);
    }

    public int Pop()
    {
        var i = _stack.Count - 1;
        if (i < 0)
            return -1;
        if (i > 0)
            _inc[i - 1] += _inc[i];
        var res = _stack.Pop() + _inc[i];
        _inc[i] = 0;
        return res;
    }

    public void Increment(int k, int val)
    {
        var i = Math.Min(k, _stack.Count) - 1;
        if (i >= 0)
            _inc[i] += val;
    }
}

[TestFixture]
public class CustomStackTests
{
    [Test]
    public void Push_PushesElement_StackContainsElement()
    {
        // Arrange
        var stack = new CustomStack(3);
        // Act
        stack.Push(1);

        // Assert
        Assert.IsTrue(stack.Pop() == 1);
    }

    [Test]
    public void Pop_EmptyStack_ReturnsMinusOne()
    {
        // Arrange
        var stack = new CustomStack(3);

        // Act
        var result = stack.Pop();

        // Assert
        Assert.IsTrue(result == -1);
    }

    [Test]
    public void Increment_IncrementsElementsInStack_ElementsIncreased()
    {
        // Arrange
        var stack = new CustomStack(3);
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);

        // Act
        stack.Increment(2, 1);

        // Assert
        Assert.IsTrue(stack.Pop() == 3);
        Assert.IsTrue(stack.Pop() == 3);
        Assert.IsTrue(stack.Pop() == 2);
    }
}