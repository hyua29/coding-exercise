namespace LeetCode.LeetCode75;

public class ReverseVowelsOfAString
{
    private ISet<char> _vowels = new HashSet<char> {'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};

    public string ReverseVowels(string s)
    {
        var results = s.ToCharArray();
        var left = 0;
        var right = s.Length - 1;

        while (left < right)
        {
            if (_vowels.Contains(results[left]) && _vowels.Contains(results[right]))
            {
                (results[left], results[right]) = (results[right], results[left]);
                right--;
                left++;
            }
            else if (_vowels.Contains(results[left]))
            {
                right--;
            }
            else if (_vowels.Contains(results[right]))
            {
                left++;
            }
            else
            {
                right--;
                left++;
            }
        }

        return string.Join("", results);
    }
}