namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-iv/#/description
/// </summary>
public class BuyStocksLimitedBuys
{
    public int MaxProfit(int k, int[] prices)
    {
        var k0 = new int[k + 1];
        var k1 = new int[k + 1];
        Array.Fill(k1, -prices[0]);

        foreach (var p in prices)
        {
            for (int i = 1; i < k1.Length; i++)
            {
                var temp1 = k1[i];
                var temp0 = k0[i];
                k0[i] = Math.Max(temp0, temp1 + p);
                k1[i] = Math.Max(temp1, k0[i - 1] - p);
            }
        }

        return k0[k];
    }
}

[TestFixture]
public class StockProfitTests
{
    private BuyStocksLimitedBuys _solution;

    [SetUp]
    public void SetUp()
    {
        _solution = new BuyStocksLimitedBuys(); // Initialize your solution class here
    }

    [Test]
    public void TestExample1()
    {
        int[] prices = {3, 2, 6, 5, 0, 3};
        int k = 2;
        int expected = 7; // The expected maximum profit for the given input

        int result = _solution.MaxProfit(k, prices);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void TestExample2()
    {
        int[] prices = {2, 4, 1};
        int k = 2;
        int expected = 2; // The expected maximum profit for the given input

        int result = _solution.MaxProfit(k, prices);

        Assert.That(result, Is.EqualTo(expected));
    }
}