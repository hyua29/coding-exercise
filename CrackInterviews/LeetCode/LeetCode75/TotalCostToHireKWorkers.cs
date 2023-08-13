namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/total-cost-to-hire-k-workers/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class TotalCostToHireKWorkers
{
    public long TotalCost(int[] costs, int k, int candidates)
    {
        long totalCost = 0;
        var l = costs.ToList();
        for (int i = 0; i < k; i++)
        {
            int removed = -1;
            long min = long.MaxValue;
            for (int j = l.Count - 1; j >= 0; j--)
            {
                if (l.Count - candidates <= j || j < candidates)
                {
                    if (l[j] <= min)
                    {
                        min = l[j];
                        removed = j;
                    }
                }
            }

            l.RemoveAt(removed);
            totalCost += min;
        }

        return totalCost;
    }
}