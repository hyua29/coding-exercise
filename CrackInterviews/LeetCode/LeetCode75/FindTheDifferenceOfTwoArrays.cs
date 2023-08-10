namespace LeetCode.LeetCode75;

public class FindTheDifferenceOfTwoArrays
{
    public IList<IList<int>> FindDifference(int[] nums1, int[] nums2)
    {
        var set1 = new HashSet<int>();
        var set2 = new HashSet<int>();

        foreach (var n in nums1)
        {
            set1.Add(n);
        }

        foreach (var n in nums2)
        {
            set2.Add(n);
        }

        var results = new List<IList<int>> {new List<int>(), new List<int>()};
        foreach (var n in set2)
        {
            if (!set1.Contains(n))
            {
                results[1].Add(n);
            }
        }

        foreach (var n in set1)
        {
            if (!set2.Contains(n))
            {
                results[0].Add(n);
            }
        }

        return results;
    }
}