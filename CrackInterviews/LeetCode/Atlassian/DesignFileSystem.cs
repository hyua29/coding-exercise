namespace LeetCode.Atlassian;

public class FileSystem
{
    private readonly Dictionary<string, int> _paths;

    public FileSystem()
    {
        Console.WriteLine();
        _paths = new Dictionary<string, int>();
    }

    public bool CreatePath(string path, int value)
    {
        if (!path.StartsWith('/'))
        {
            return false;
        }

        if (_paths.Keys.Contains(path))
        {
            return false;
        }
        else
        {
            var subPaths = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (subPaths.Length > 1)
            {
                var s = string.Empty;
                for (int i = 0; i < subPaths.Length - 1; i++)
                {
                    s += $"/{subPaths[i]}";
                    if (!_paths.Keys.Contains(s))
                    {
                        return false;
                    }
                }
            }
        }

        _paths.Add(path, value);
        return true;
    }

    public int Get(string path)
    {
        if (_paths.TryGetValue(path, out var results))
        {
            return results;
        }
        else
        {
            return -1;
        }
    }
}

[TestFixture]
public class FileSystemTests
{
    [Test]
    public void Test1()
    {
        var fs = new FileSystem();

        Assert.IsTrue(fs.CreatePath("/leet", 1));
        Assert.IsTrue(fs.CreatePath("/leet/code", 2));
        Assert.That(fs.Get("/leet/code"), Is.EqualTo(2));
        Assert.IsFalse(fs.CreatePath("/c/d", 1));
    }
    
    [Test]
    public void Test2()
    {
        var fs = new FileSystem();

        Assert.IsTrue(fs.CreatePath("/leet", 1));
        Assert.IsTrue(fs.CreatePath("/leet/code", 2));
        Assert.That(fs.Get("/leet/code"), Is.EqualTo(2));
        Assert.IsTrue(fs.CreatePath("/leet/code", 3));
        Assert.That(fs.Get("/leet/code"), Is.EqualTo(3));
    }
}