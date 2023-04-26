namespace LeetCode.Atlassian;

public class TopKFrequentWords
{
    public IList<string> TopKFrequent(string[] words, int k)
    {
        var dic = new Dictionary<string, int>();

        foreach (var w in words)
        {
            if (!dic.TryAdd(w, 1))
            {
                dic[w]++;
            }
        }

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
    private TopKFrequentWords _topKFrequentWords;

    [SetUp]
    public void SetUp()
    {
        _topKFrequentWords = new TopKFrequentWords();
    }

    [Test]
    public void TopKFrequentWords_ReturnsTopKWords_WhenKIsLessThanWordsCount()
    {
        // Arrange
        string[] words = {"hello", "world", "hello", "world", "hello", "programming", "programming"};
        int k = 2;
        List<string> expected = new List<string> {"hello", "programming"};

        // Act
        IList<string> actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsAllWords_WhenKIsGreaterThanWordsCount1()
    {
        // Arrange
        string[] words = {"the", "day", "is", "sunny", "the", "the", "the", "sunny", "is", "is"};
        int k = 4;
        List<string> expected = new List<string> {"the", "is", "sunny", "day"};

        // Act
        IList<string> actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsAllWords_WhenKIsGreaterThanWordsCount()
    {
        // Arrange
        string[] words = {"hello", "world", "hello", "world", "hello", "programming", "programming"};
        int k = 10;
        List<string> expected = new List<string> {"hello", "programming", "world"};

        // Act
        IList<string> actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsEmptyList_WhenWordsIsEmpty()
    {
        // Arrange
        string[] words = { };
        int k = 2;
        List<string> expected = new List<string> { };

        // Act
        IList<string> actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TopKFrequentWords_ReturnsEmptyList_WhenKIsZero()
    {
        // Arrange
        string[] words = {"hello", "world", "hello", "world", "hello", "programming", "programming"};
        int k = 0;
        List<string> expected = new List<string> { };

        // Act
        IList<string> actual = _topKFrequentWords.TopKFrequent(words, k);

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}