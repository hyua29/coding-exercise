namespace LeetCode.LeetCode150;

public class LongestConsecutiveSequence
{
    public int LongestConsecutive(int[] nums)
    {
        var longest = 0;

        var set = new HashSet<int>(nums);

        foreach (var s in set)
        {
            var count = 1;

            if (!set.Contains(s - 1))
            {
                var next = s + 1;
                while (set.Contains(next))
                {
                    next++;
                    count++;
                }
            }

            longest = Math.Max(longest, count);
        }

        return longest;
    }
}