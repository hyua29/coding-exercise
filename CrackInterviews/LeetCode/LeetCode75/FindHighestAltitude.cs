namespace LeetCode.LeetCode75;

public class FindHighestAltitude
{
    public int LargestAltitude(int[] gain)
    {
        int maxAltitude = 0;
        int currentAltitude = 0;

        foreach (var g in gain)
        {
            currentAltitude += g;
            maxAltitude = Math.Max(maxAltitude, currentAltitude);
        }

        return maxAltitude;
    }
}