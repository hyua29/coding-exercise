namespace LeetCode.LeetCode150;

/// <summary>
/// https://leetcode.com/problems/gas-station/?envType=study-plan-v2&amp;envId=top-interview-150
/// </summary>
public class GasStation
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        if (gas.Length != cost.Length) throw new ArgumentException();

        var possibleStartingIndexes = new PriorityQueue<int, int>();
        for (int i = 0; i < gas.Length; i++)
        {
            var diff = gas[i] - cost[i];
            if (diff >= 0) possibleStartingIndexes.Enqueue(i, -diff);
        }

        var feasibleIndex = -1;
        while (feasibleIndex == -1 && possibleStartingIndexes.TryDequeue(out var startingIndex, out _))
        {
            var spareGas = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                var index = (startingIndex + i) % gas.Length;

                spareGas = spareGas + gas[index] - cost[index];
                if (spareGas < 0) break;
            }

            if (spareGas >= 0)
            {
                feasibleIndex = startingIndex;
            }
        }

        return feasibleIndex;
    }
}