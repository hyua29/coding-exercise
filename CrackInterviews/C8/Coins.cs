using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace C8;

using System.Diagnostics;

/// <summary>
/// See explanation here: https://leetcode.com/problems/coin-change-ii/solutions/141076/unbounded-knapsack/?orderBy=most_votes
/// </summary>
public class Coins
{
    public static int Calculate(int[] coins, int amount)
    {
        var unique = new HashSet<int>(coins).ToArray();

        int[] dp = new int[amount + 1];
        dp[0] = 1;        
        for (int j = 0; j < unique.Length; j++) {
            for (int i = 1; i <= amount; i++) {
                if (i - unique[j] >= 0) {
                    dp[i] += dp[i - unique[j]];
                }
            }
        }
        return dp[amount];
    }
}

[TestFixture]
public class CoinChangeTests 
{
    [Test]
    public void TestExampleCase0() 
    {
        int[] coins = new int[] { 1,5 };
        int amount = 10;
        int expected = 3;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestExampleCase() 
    {
        int[] coins = new int[] { 1, 5, 10, 25 };
        int amount = 10;
        int expected = 4;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void TestOneCoinDenomination() 
    {
        int[] coins = new int[] { 5 };
        int amount = 15;
        int expected = 1;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestAmountNotPossible() 
    {
        int[] coins = new int[] { 5, 10, 25 };
        int amount = 7;
        int expected = 0;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void TestAmountZero() 
    {
        int[] coins = new int[] { 1, 5, 10, 25 };
        int amount = 0;
        int expected = 1;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestEmptyCoinsList() 
    {
        int[] coins = new int[] { };
        int amount = 10;
        int expected = 0;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void TestCoinsListWithDuplicates() 
    {
        int[] coins = new int[] { 1, 5, 10, 5 };
        int amount = 15;
        int expected = 6;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
}
