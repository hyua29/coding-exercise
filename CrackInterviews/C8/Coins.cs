using NUnit.Framework;

namespace C8;

using System.Diagnostics;

public class Coins
{
    public static int Calculate(int[] availableValues, int targetCents)
    {
        Debug.Assert(targetCents >= 0);

        var buffer = new int [targetCents + 1];
        buffer[0] = 1;

        for (int i = 1; i <= targetCents; i++)
        {
            foreach (var v in availableValues)
            {
                var previous = i - v;
                if (previous >= 0)
                {
                    buffer[i] += buffer[previous];
                }
            }
        }

        return buffer[^1];
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
        int expected = 1;
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
        int expected = 3;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
    
    [Test]
    public void TestMultipleOfSmallestCoin() 
    {
        int[] coins = new int[] { 1, 2, 5, 10 };
        int amount = 20;
        int expected = 2;
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
        int expected = 4;
        int actual = Coins.Calculate(coins, amount);
        Assert.AreEqual(expected, actual);
    }
}
