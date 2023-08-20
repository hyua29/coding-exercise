namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/koko-eating-bananas/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class KokoEatingBananas
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        var low = 1;
        var high = piles.Max();

        var minK = int.MaxValue;
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            if (CanEatAll(piles,mid, h))
            {
                minK = Math.Min(mid, minK);
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return minK;
    }

    private static bool CanEatAll(int[] piles, int k, int h)
    {
        long countHour = 0;
        
        foreach (int pile in piles) {
            countHour += pile / k;
            if (pile % k != 0)
                countHour++;
        }

        return countHour <= h;
    }
}