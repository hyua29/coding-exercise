namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/asteroid-collision/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class AsteroidCollisionProblem
{
    public int[] AsteroidCollision(int[] asteroids)
    {
        var s = new Stack<int>();

        foreach (var a in asteroids)
        {
            if (a > 0)
            {
                s.Push(a);
                continue;
            }

            var incomingDestroyed = false;
            while (s.TryPeek(out var top) && top > 0 && !incomingDestroyed)
            {
                // incoming got destroyed
                if (top > -a)
                {
                    incomingDestroyed = true;
                    break;
                }

                // both got destroyed
                if (top == -a)
                {
                    s.Pop();
                    incomingDestroyed = true;
                    break;
                }

                // incoming destroyed existing and continued moving
                if (top < -a)
                {
                    s.Pop();
                }
            }

            if (!incomingDestroyed)
            {
                s.Push(a);
            }
        }

        return s.Reverse().ToArray();
    }
}

[TestFixture]
public class AsteroidCollisionTests
{
    private AsteroidCollisionProblem _solution;

    [SetUp]
    public void Setup()
    {
        _solution = new AsteroidCollisionProblem();
    }

    [Test]
    public void Test_AsteroidCollision_Example1()
    {
        var asteroids = new int[] {5, 10, -5};
        var expected = new int[] {5, 10};
        var result = _solution.AsteroidCollision(asteroids);
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [Test]
    public void Test_AsteroidCollision_Example2()
    {
        var asteroids = new int[] {8, -8};
        var expected = new int[] { };
        var result = _solution.AsteroidCollision(asteroids);
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [Test]
    public void Test_AsteroidCollision_Example3()
    {
        var asteroids = new int[] {10, 2, -5};
        var expected = new int[] {10};
        var result = _solution.AsteroidCollision(asteroids);
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [Test]
    public void Test_AsteroidCollision_Example4()
    {
        var asteroids = new int[] {-2, -1, 1, 2};
        var expected = new int[] {-2, -1, 1, 2};
        var result = _solution.AsteroidCollision(asteroids);
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [Test]
    public void Test_AsteroidCollision_Example5()
    {
        var asteroids = new int[] {-2, -2, 1, -2};
        var expected = new int[] {-2, -2, -2};
        var result = _solution.AsteroidCollision(asteroids);
        Assert.IsTrue(expected.SequenceEqual(result));
    }

    [Test]
    public void Test_AsteroidCollision_Example6()
    {
        var asteroids = new int[] {1, -2, -2, -2};
        var expected = new int[] {-2, -2, -2};
        var result = _solution.AsteroidCollision(asteroids);
        Assert.IsTrue(expected.SequenceEqual(result));
    }
}