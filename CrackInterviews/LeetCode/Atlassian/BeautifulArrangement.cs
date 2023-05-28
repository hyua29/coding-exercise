namespace LeetCode.Atlassian;

/// <summary>
/// https://leetcode.com/problems/beautiful-arrangement/
/// </summary>
public class BeautifulArrangement
{
    public int CountArrangement(int n)
    {
        var results = 0;
        CountArrangementAux(n, new HashSet<int>(), ref results);
        return results;
    }

    private void CountArrangementAux(int size, ISet<int> buffer, ref int results)
    {
        if (buffer.Count == size)
        {
            results++;
        }

        var index = buffer.Count + 1;
        for (int i = size; i >= 1; i--)
        {
            if (!buffer.Contains(i) && (i % index == 0 || index % i == 0))
            {
                buffer.Add(i);
                CountArrangementAux(size, buffer, ref results);
                buffer.Remove(i);
            }
        }
    }
}