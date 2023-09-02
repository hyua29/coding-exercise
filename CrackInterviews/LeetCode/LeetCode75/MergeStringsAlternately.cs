namespace LeetCode.LeetCode75;

using System.Text;

/// <summary>
/// https://leetcode.com/problems/merge-strings-alternately/?envType=study-plan-v2&envId=leetcode-75
/// </summary>
public class MergeStringsAlternately
{
    public string MergeAlternately(string word1, string word2)
    {
        var buffer = new StringBuilder();
        var i = 0;
        while (i < word1.Length || i < word2.Length)
        {
            if (i < word1.Length)
            {
                buffer.Append(word1[i]);
            }
            else if (i < word2.Length)
            {
                buffer.Append(word2.Substring(i));
                break;
            }

            if (i < word2.Length)
            {
                buffer.Append(word2[i]);
            }
            else if (i + 1 < word1.Length)
            {
                buffer.Append(word1.Substring(i + 1));
                break;
            }

            i++;
        }

        return buffer.ToString();
    }
}