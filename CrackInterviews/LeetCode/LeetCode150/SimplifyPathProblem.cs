namespace LeetCode.LeetCode150;

public class SimplifyPathProblem
{
    public string SimplifyPath(string path)
    {
        var modifiedPath = path.Trim('/');

        var buffer = new Stack<string>();
        var entities = modifiedPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
        foreach (var e in entities)
        {
            if (e.Equals("..", StringComparison.Ordinal))
            {
                buffer.TryPop(out _);
                continue;
            }

            if (e != ".")
            {
                buffer.Push(e);
            }
        }

        return $"/{string.Join('/', buffer.Reverse())}";
    }
}

[TestFixture]
public class SimplifyPathTests
{
    private SimplifyPathProblem _s = new SimplifyPathProblem();

    [Test]
    public void Test1()
    {
        string path = "/a/./b/../../c/";
        string expected = "/c";
        string simplifiedPath = _s.SimplifyPath(path);
        Assert.That(simplifiedPath, Is.EqualTo(expected));
    }

    [Test]
    public void Test2()
    {
        string path = "/a/../../b/../c//.//";
        string expected = "/c";
        string simplifiedPath = _s.SimplifyPath(path);
        Assert.That(simplifiedPath, Is.EqualTo(expected));
    }

    [Test]
    public void Test3()
    {
        string path = "/a//b////c/d//././/..";
        string expected = "/a/b/c";
        string simplifiedPath = _s.SimplifyPath(path);
        Assert.That(simplifiedPath, Is.EqualTo(expected));
    }
}