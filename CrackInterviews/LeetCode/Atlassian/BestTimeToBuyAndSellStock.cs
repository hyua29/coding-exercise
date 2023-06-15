namespace LeetCode.Atlassian;

public class BestTimeToBuyAndSellStock
{
    public int MaxProfit(int[] prices)
    {
        var lowest = int.MaxValue;
        var maxProfit = 0;
        foreach (var p in prices)
        {
            if (p < lowest)
            {
                lowest = p;
            }

            var currentProfit = p - lowest;
            if (currentProfit > maxProfit)
            {
                maxProfit = currentProfit;
            }
        }

        return maxProfit;
    }
}

[TestFixture]
public class MaxProfitTests
{
    [Test]
    public void Test1_ReturnsCorrectResult()
    {
        // Arrange
        int[] prices = { 7, 1, 5, 3, 6, 4 };
        int expected = 5;
        var solution = new BestTimeToBuyAndSellStock();

        // Act
        int result = solution.MaxProfit(prices);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test2_ReturnsCorrectResult()
    {
        // Arrange
        int[] prices = { 7, 6, 4, 3, 1 };
        int expected = 0;
        var solution = new BestTimeToBuyAndSellStock();

        // Act
        int result = solution.MaxProfit(prices);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Test3_ReturnsCorrectResult()
    {
        // Arrange
        int[] prices = { 5, 4, 3, 2, 1 };
        int expected = 0;
        var solution = new BestTimeToBuyAndSellStock();

        // Act
        int result = solution.MaxProfit(prices);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}
