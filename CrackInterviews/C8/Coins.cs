namespace C8;

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

/// <summary>
///     See explanation here:
///     https://leetcode.com/problems/coin-change-ii/solutions/141076/unbounded-knapsack/?orderBy=most_votes
///     and here:
///     https://leetcode.com/problems/coin-change-ii/solutions/176706/beginner-mistake-why-an-inner-loop-for-coins-doensn-t-work-java-soln/?orderBy=most_votes
/// </summary>
public class Coins
{
    public static int Calculate(int[] coins, int amount)
    {
        var unique = new HashSet<int>(coins).ToArray();

        var dp = new int[amount + 1];
        dp[0] = 1;
        for (var j = 0; j < unique.Length; j++)
        for (var i = 1; i <= amount; i++)
            if (i - unique[j] >= 0)
                dp[i] += dp[i - unique[j]];

        return dp[amount];
    }
}

[TestFixture]
public class CoinChangeTests
{
    [Test]
    public void TestExampleCase0()
    {
        // 5,5
        int[] coins = {1, 5};
        var amount = 10;
        var expected = 3;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestExampleCase()
    {
        int[] coins = {1, 5, 10, 25};
        var amount = 10;
        var expected = 4;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestOneCoinDenomination()
    {
        int[] coins = {5};
        var amount = 15;
        var expected = 1;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestAmountNotPossible()
    {
        int[] coins = {5, 10, 25};
        var amount = 7;
        var expected = 0;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestAmountZero()
    {
        int[] coins = {1, 5, 10, 25};
        var amount = 0;
        var expected = 1;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestEmptyCoinsList()
    {
        int[] coins = { };
        var amount = 10;
        var expected = 0;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestCoinsListWithDuplicates()
    {
        int[] coins = {1, 5, 10, 5};
        var amount = 15;
        var expected = 6;
        var actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
}