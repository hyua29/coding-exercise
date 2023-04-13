namespace LeetCode.Atlassian;

public class SnakeGame
{
    public const string LEFT = "L";
    public const string RIGHT = "R";
    public const string UP = "U";
    public const string DOWN = "D";

    private readonly int _width;
    private readonly int _height;
    private readonly int[][] _food;
    private readonly LinkedList<(int Y, int X)> _snakeLocations;
    private int _round;
    private int _score;

    public SnakeGame(int width, int height, int[][] food)
    {
        _snakeLocations = new LinkedList<(int Y, int X)>();
        _snakeLocations.AddFirst((0, 0));
        _score = 0;
        _round = 0;
        _width = width;
        _height = height;
        _food = food;
    }

    public int Move(string direction)
    {
        var headLocation = _snakeLocations.First!;
        var newHeadLocation = GetNewHeadLocation(direction, headLocation);

        if (IsHeadOutOfBound(newHeadLocation))
        {
            Console.WriteLine($"new head location: {newHeadLocation.X}, {newHeadLocation.Y}");
            return -1;
        }

        if (IsHeadOutOfBound(newHeadLocation))
        {
            Console.WriteLine($"new head location: {newHeadLocation.X}, {newHeadLocation.Y}");
            return -1;
        }

        _snakeLocations.AddBefore(headLocation, new LinkedListNode<(int Y, int X)>(newHeadLocation));

        // Remove the tail if the new position the head has moved to doesn't have food
        var currentHeadLocation = _snakeLocations.First!.Value;
        Console.WriteLine($"current head location: X - {currentHeadLocation.X}, Y - {currentHeadLocation.Y}");
        if (_score < _food.Length && _food[_score][0] == currentHeadLocation.Y &&
            _food[_score][1] == currentHeadLocation.X)
        {
            Console.WriteLine($"Found food: {currentHeadLocation.X}, {currentHeadLocation.Y}");
            _score++;
        }
        else
        {
            var tailLocation = _snakeLocations.Last!;
            _snakeLocations.Remove(tailLocation);
        }

        if (HasHeadHitBody(newHeadLocation)) return -1;
        
        _round++;

        return _score;
    }

    private bool HasHeadHitBody((int Y, int X) newHeadLocation)
    {
        var current = _snakeLocations.First!.Next;
        while (current != null)
        {
            if (current.Value.X == newHeadLocation.X && current.Value.Y == newHeadLocation.Y)
            {
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    private bool IsHeadOutOfBound((int Y, int X) newHeadLocation)
    {
        return newHeadLocation.X < 0 || newHeadLocation.X >= _width || newHeadLocation.Y < 0 ||
               newHeadLocation.Y >= _height;
    }

    private static (int Y, int X) GetNewHeadLocation(string direction, LinkedListNode<(int Y, int X)> headLocation)
    {
        (int Y, int X) newHeadLocation;
        if (direction.Equals(LEFT, StringComparison.Ordinal))
        {
            newHeadLocation = (headLocation.Value.Y, headLocation.Value.X - 1);
        }
        else if (direction.Equals(RIGHT, StringComparison.Ordinal))
        {
            newHeadLocation = (headLocation.Value.Y, headLocation.Value.X + 1);
        }
        else if (direction.Equals(UP, StringComparison.Ordinal))
        {
            newHeadLocation = (headLocation.Value.Y - 1, headLocation.Value.X);
        }
        else if (direction.Equals(DOWN, StringComparison.Ordinal))
        {
            newHeadLocation = (headLocation.Value.Y + 1, headLocation.Value.X);
        }
        else
        {
            throw new InvalidOperationException($"Received value is: {direction}");
        }

        return newHeadLocation;
    }

    public class SnakeGameTests
    {
        [Test]
        public void Test1()
        {
            var snakeGame = new SnakeGame(2, 2, new int[1][] {new[] {0, 1}});
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(1));
            Assert.That(snakeGame.Move(DOWN), Is.EqualTo(1));
        }

        [Test]
        public void Test2()
        {
            var snakeGame = new SnakeGame(3, 2, new int[2][] {new[] {1, 2}, new[] {0, 1}});
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(0));
            Assert.That(snakeGame.Move(DOWN), Is.EqualTo(0));
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(1));
            Assert.That(snakeGame.Move(UP), Is.EqualTo(1));
            Assert.That(snakeGame.Move(LEFT), Is.EqualTo(2));
            Assert.That(snakeGame.Move(UP), Is.EqualTo(-1));
        }

        [Test]
        public void Test3()
        {
            var snakeGame = new SnakeGame(3, 3,
                new int[][] {new[] {2, 0}, new[] {0, 0}, new[] {0, 2}, new[] {0, 1}, new[] {2, 2}, new[] {0, 1}});
            Assert.That(snakeGame.Move(DOWN), Is.EqualTo(0));
            Assert.That(snakeGame.Move(DOWN), Is.EqualTo(1));
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(1));
            Assert.That(snakeGame.Move(UP), Is.EqualTo(1));
            Assert.That(snakeGame.Move(UP), Is.EqualTo(1));
            Assert.That(snakeGame.Move(LEFT), Is.EqualTo(2));
            Assert.That(snakeGame.Move(DOWN), Is.EqualTo(2));
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(2));
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(2));
            Assert.That(snakeGame.Move(UP), Is.EqualTo(3));
            Assert.That(snakeGame.Move(LEFT), Is.EqualTo(4));
            Assert.That(snakeGame.Move(LEFT), Is.EqualTo(4));
            Assert.That(snakeGame.Move(DOWN), Is.EqualTo(4));
            Assert.That(snakeGame.Move(RIGHT), Is.EqualTo(4));
            Assert.That(snakeGame.Move(UP), Is.EqualTo(-1));
        }
    }
}