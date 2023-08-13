namespace LeetCode.LeetCode75;

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