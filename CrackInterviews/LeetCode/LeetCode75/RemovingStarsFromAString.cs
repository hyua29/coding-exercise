namespace LeetCode.LeetCode75;

public class RemovingStarsFromAString
{
    public string RemoveStars(string s) {
        var stack  = new Stack<char>();

        foreach (var c in s)
        {
            if (c != '*')
            {
                stack.Push(c);
            } 
            else
            {
                stack.Pop();
            }
        }

        return string.Join(string.Empty, stack.Reverse());
    }
}