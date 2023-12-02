namespace LeetCode.LeetCode150;

/// <summary>
/// https://leetcode.com/problems/gas-station/?envType=study-plan-v2&amp;envId=top-interview-150
/// </summary>
public class GasStation
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        if (gas.Length != cost.Length) throw new ArgumentException();

        var result = -1;
        var startingIndex = 0;
        while (startingIndex < gas.Length)
        {
            var surplus = gas[startingIndex];
            var hasReachedEnd = true;
            for (int offset = 0; offset < gas.Length; offset++)
            {
                var currentIndex = (startingIndex + offset) % gas.Length;
                surplus -= cost[currentIndex];
                if (surplus < 0)
                {
                    // can't proceed
                    startingIndex = startingIndex + offset + 1;
                    hasReachedEnd = false;
                    break;
                }

                surplus += gas[(currentIndex + 1) % gas.Length];
            }

            if (hasReachedEnd)
            {
                result = startingIndex;
                break;
            }
        }

        return result;
    }
}

public class GasStationTest
{
    [Test]
    public void Test_1()
    {
        var sol = new GasStation();

        var gas = new[] {1, 2, 3, 4, 5};
        var cost = new[] {3, 4, 5, 1, 2};
        Assert.That(sol.CanCompleteCircuit(gas, cost), Is.EqualTo(3));
    }
    [Test]
    public void Test_2()
    {
        var sol = new GasStation();

        var gas = new[] {2,3,4};
        var cost = new[] {3, 4, 3};
        Assert.That(sol.CanCompleteCircuit(gas, cost), Is.EqualTo(-1));
    }
}