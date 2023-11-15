namespace LeetCode.LeetCode150;

public class FindKPairsWithSmallestSums
{
    public IList<IList<int>> KSmallestPairs(int[] nums1, int[] nums2, int k)
    {
        var pq = new PriorityQueue<(int A, int B, int BIndex), int>();

        foreach (var n1 in nums1)
        {
            pq.Enqueue((n1, nums2[0], 0), n1 + nums2[0]);
        }

        var results = new List<IList<int>>();

        for (int i = 0; i < k && pq.TryDequeue(out var item, out _); i++)
        {
            results.Add(new List<int> {item.A, item.B});

            if (item.BIndex + 1 < nums2.Length)
            {
                pq.Enqueue((item.A, nums2[item.BIndex + 1], item.BIndex + 1), item.A + nums2[item.BIndex + 1]);
            }
        }

        return results;
    }
}