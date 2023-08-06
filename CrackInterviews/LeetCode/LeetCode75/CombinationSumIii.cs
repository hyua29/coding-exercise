namespace LeetCode.LeetCode75;

public class CombinationSumIii
{
    public IList<IList<int>> CombinationSum3(int k, int n) {
        var results = new List<IList<int>>();

        Aux(k, n, 1, new List<int>(), results);

        return results;
    }

    private void Aux(int k, int n, int position, IList<int> temp, IList<IList<int>> results)
    {
        if (temp.Sum() == n)
        {
            if (temp.Count == k)
            {
                results.Add(new List<int>(temp));
            }

            return;
        }

        for (int i=position; i<=9; i++)
        {
            if (temp.Sum() + i <= n && temp.Count <= k)
            {
                temp.Add(i);
                Aux(k, n, i + 1, temp, results);
                temp.RemoveAt(temp.Count - 1);
            }
        }
    }
}