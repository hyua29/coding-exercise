namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/domino-and-tromino-tiling/solutions/2944069/video/?envType=study-plan-v2&envId=leetcode-75
/// https://leetcode.com/problems/domino-and-tromino-tiling/solutions/116581/detail-and-explanation-of-o-n-solution-why-dp-n-2-d-n-1-dp-n-3/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class DominoAndTrominoTiling
{
    public int NumTilings(int n) {
        if (n == 0) return 0;
        if (n == 1) return 1;
        if (n == 2) return 2;
        if (n == 3) return 5;

        var buffer = new int[n + 1];
        buffer[0] = 1;
        buffer[1] = 1;
        buffer[2] = 2;
        buffer[3] = 5;
        
        for (int i = 4; i <= n; i++)
        {
            buffer[i] = (2*buffer[i-1] + buffer[i-3]) % 1000000007;
        }

        return buffer[n];
    }
}