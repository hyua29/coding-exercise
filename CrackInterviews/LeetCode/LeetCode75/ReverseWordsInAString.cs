namespace LeetCode.LeetCode75;

public class ReverseWordsInAString
{
    public string ReverseWords(string s)
    {
        var result = s.Trim().Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Reverse();
        return string.Join(" ", result);
    }
}