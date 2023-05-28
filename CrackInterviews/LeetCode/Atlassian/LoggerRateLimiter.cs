namespace LeetCode.Atlassian;

public class Logger
{
    private readonly int[] _buckets;
    private readonly HashSet<string>[] _sets;

    public Logger()
    {
        _buckets = new int[10];
        _sets = new HashSet<string>[10];
        for (var i = 0; i < _sets.Length; ++i) _sets[i] = new HashSet<string>();
    }

    public bool ShouldPrintMessage(int timestamp, string message)
    {
        var idx = timestamp % 10;
        if (timestamp != _buckets[idx])
        {
            _sets[idx].Clear();
            _buckets[idx] = timestamp;
        }

        for (var i = 0; i < _buckets.Length; ++i)
            if (timestamp - _buckets[i] < 10)
                if (_sets[i].Contains(message))
                    return false;

        _sets[idx].Add(message);
        return true;
    }
}

[TestFixture]
public class LoggerTests
{
    [Test]
    public void LoggerTest()
    {
        var logger = new Logger();

        Assert.That(logger.ShouldPrintMessage(1, "foo"),
            Is.EqualTo(true)); // return true, next allowed timestamp for "foo" is 1 + 10 = 11
        Assert.That(logger.ShouldPrintMessage(1, "foo"),
            Is.EqualTo(false)); // return true, next allowed timestamp for "foo" is 1 + 10 = 11
        Assert.That(logger.ShouldPrintMessage(1, "bar"),
            Is.EqualTo(true)); // return true, next allowed timestamp for "foo" is 1 + 10 = 11
        Assert.That(logger.ShouldPrintMessage(2, "bar"),
            Is.EqualTo(false)); // return true, next allowed timestamp for "bar" is 2 + 10 = 12
        Assert.That(logger.ShouldPrintMessage(3, "foo"), Is.EqualTo(false)); // 3 < 11, return false
        Assert.That(logger.ShouldPrintMessage(8, "bar"), Is.EqualTo(false)); // 8 < 12, return false
        Assert.That(logger.ShouldPrintMessage(10, "foo"), Is.EqualTo(false)); // 10 < 11, return false
        Assert.That(logger.ShouldPrintMessage(11, "foo"),
            Is.EqualTo(true)); // 11 >= 11, return true, next allowed timestamp for "foo" is 11 + 10 = 21
        Assert.That(logger.ShouldPrintMessage(11, "bar"),
            Is.EqualTo(true)); // 11 >= 11, return true, next allowed timestamp for "foo" is 11 + 10 = 21
        Assert.That(logger.ShouldPrintMessage(11, "foo"), Is.EqualTo(false));
        Assert.That(logger.ShouldPrintMessage(11, "bar"), Is.EqualTo(false));
    }
}