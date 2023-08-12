namespace LeetCode.LeetCode75;

/// <summary>
/// https://leetcode.com/problems/determine-if-two-strings-are-close/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class DetermineTwoStringsAreClose
{
    public bool CloseStrings(string word1, string word2)
    {
        if (word1.Length != word2.Length)
        {
            return false;
        }

        var dict1 = new Dictionary<char, int>();
        var dict2 = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (dict1.ContainsKey(c))
            {
                dict1[c] += 1;
            }
            else
            {
                dict1[c] = 1;
            }
        }

        foreach (char c in word2)
        {
            if (dict2.ContainsKey(c))
            {
                dict2[c] += 1;
            }
            else
            {
                dict2[c] = 1;
            }
        }

        if (dict1.Count != dict2.Count)
        {
            return false;
        }

        foreach (var k in dict1.Keys)
        {
            if (!dict2.ContainsKey(k)) return false;
        }

        var v1 = dict1.Values.ToArray();
        Array.Sort(v1);

        var v2 = dict2.Values.ToArray();
        Array.Sort(v2);

        for (int i = 0; i < v1.Length; i++)
        {
            if (v1[i] != v2[i]) return false;
        }

        return true;
    }
}