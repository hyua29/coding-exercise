namespace LeetCode.Atlassian;

using System.Diagnostics;

public class ArrangingCoins
{
    public int ArrangeCoins(int n)
    {
        Debug.Assert(n > 0);

        return (int) (Math.Sqrt(2 * (long) n + 0.25) - 0.5);
    }
}