namespace LeetCode.LeetCode75;

public class MinCostClimbingStairsClass
{
    public int MinCostClimbingStairs(int[] cost)
    {
        if (cost.Length <= 2) return 0;
        var cost1 = 0;
        var cost2 = 0;

        var picked = cost1;
        for (int i = 0; i < cost.Length - 1; i++)
        {
            if (i % 2 == 0)
            {
                cost2 = Math.Min(cost1 + cost[i + 1], cost2 + cost[i]);
                picked = cost2;
            }
            else
            {
                cost1 = Math.Min(cost2 + cost[i + 1], cost1 + cost[i]);
                picked = cost1;
            }
        }

        return picked;
    }
}