namespace LeetCode.Atlassian;

public class TopKFrequentWords
{
    public IList<string> TopKFrequent(string[] words, int k)
    {
        var dic = new Dictionary<string, int>();

        foreach (var w in words)
            if (!dic.TryAdd(w, 1))
                dic[w]++;

        var results = dic
            .OrderByDescending(d => d.Value).ThenBy(d => d.Key)
            .Select(x => x.Key)
            .Take(k)
            .ToList();

        return results;
    }
}

[TestFixture]
public class TopKFrequentWordsTests
{
    [SetUp]
    public void SetUp()
    {
        _topKFrequentWords = new TopKFrequentWords();
    }

    private TopKFrequentWords _topKFrequentWords;

    [Test]
    public void TopKFrequentWords_ReturnsTopKWords_WhenKIsLessThanWordsCount()
    {
        // Arrange
        string[] words = {"hello", "world", "hello", "world", "hello", "programming", "programming"};
        var k = 2;
        var expected = new List<string> {"hello", "programming"};

        // Act
        var actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsAllWords_WhenKIsGreaterThanWordsCount1()
    {
        // Arrange
        string[] words = {"the", "day", "is", "sunny", "the", "the", "the", "sunny", "is", "is"};
        var k = 4;
        var expected = new List<string> {"the", "is", "sunny", "day"};

        // Act
        var actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsAllWords_WhenKIsGreaterThanWordsCount()
    {
        // Arrange
        string[] words = {"hello", "world", "hello", "world", "hello", "programming", "programming"};
        var k = 10;
        var expected = new List<string> {"hello", "programming", "world"};

        // Act
        var actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsEmptyList_WhenWordsIsEmpty()
    {
        // Arrange
        string[] words = { };
        var k = 2;
        var expected = new List<string>();

        // Act
        var actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsEmptyList_WhenKIsZero()
    {
        // Arrange
        string[] words = {"hello", "world", "hello", "world", "hello", "programming", "programming"};
        var k = 0;
        var expected = new List<string>();

        // Act
        var actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}