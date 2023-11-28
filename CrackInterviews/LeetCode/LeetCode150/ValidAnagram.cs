namespace LeetCode.LeetCode150;

/// <summary>
/// https://leetcode.com/problems/valid-anagram/?envType=study-plan-v2&amp;envId=top-interview-150
/// </summary>
public class ValidAnagram
{
    public bool IsAnagram(string s, string t)
    {
        var dict1 = new Dictionary<char, int>();
        foreach (var sc in s)
        {
            if (!dict1.TryAdd(sc, 1))
            {
                dict1[sc]++;
            }
        }

        foreach (var tc in t)
        {
            if (!dict1.ContainsKey(tc))
            {
                return false;
            }

            dict1[tc]--;

            if (dict1[tc] == 0)
            {
                dict1.Remove(tc);
            }
        }

        return dict1.Count == 0;
    }
}