namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee/
/// https://leetcode.com/problems/best-time-to-buy-and-sell-stock-with-transaction-fee/solutions/108870/most-consistent-ways-of-dealing-with-the-series-of-stock-problems/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class BuyStocksWithFee
{
    public int MaxProfit(int[] prices, int fee)
    {
        var dp = new int[prices.Length + 1, 2];
        dp[0, 1] = -prices[0];
        for (int i = 1; i < dp.GetLength(0); i++)
        {
            dp[i, 1] = Math.Max(dp[i - 1, 1], dp[i - 1, 0] - prices[i - 1]);
            dp[i, 0] = Math.Max(dp[i - 1, 0], dp[i - 1, 1] + prices[i - 1] - fee);
        }

        return dp[dp.GetLength(0) - 1, 0];
    }
    
    public int MaxProfitWithBetterSpace(int[] prices, int fee)
    {
        var bufferHasStock = -prices[0];
        var bufferNoStock = 0;
        
        for (int i = 0; i < prices.Length ; i++)
        {
            var temp = bufferHasStock;
            bufferHasStock = Math.Max(bufferHasStock, bufferNoStock - prices[i]);
            bufferNoStock = Math.Max(bufferNoStock, temp + prices[i] - fee);
        }

        return bufferNoStock;
    }
}

[TestFixture]
public class Tests
{
    private BuyStocksWithFee solution;

    [SetUp]
    public void Setup()
    {
        solution = new BuyStocksWithFee();
    }

    [Test]
    public void BuyStocksWithFee1()
    {
        int[] prices = {1, 3, 2, 8, 4, 9};
        int fee = 2;
        int expected = 8;
        int result = solution.MaxProfit(prices, fee);
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void BuyStocksWithFee2()
    {
        int[] prices = {1, 3, 7, 5, 10, 3};
        int fee = 3;
        int expected = 6;
        int result = solution.MaxProfit(prices, fee);
        Assert.That(result, Is.EqualTo(expected));
    }
}