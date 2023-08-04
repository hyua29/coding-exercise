namespace LeetCode.LeetCode75;

public class LetterCombinationsOfAPhoneNumber
{
    private static readonly Dictionary<int, List<string>> Map = new Dictionary<int, List<string>>()
    {
        {2, new List<string> {"a", "b", "c"}},
        {3, new List<string> {"d", "e", "f"}},
        {4, new List<string> {"g", "h", "i"}},
        {5, new List<string> {"j", "k", "l"}},
        {6, new List<string> {"m", "n", "o"}},
        {7, new List<string> {"p", "q", "r", "s"}},
        {8, new List<string> {"t", "u", "v"}},
        {9, new List<string> {"w", "x", "y", "z"}}
    };

    public IList<string> LetterCombinations(string digits)
    {
        IList<string> results = new List<string>();

        var s = string.Empty;

        foreach (var d in digits)
        {
            var number = d - '0';

            if (Map.ContainsKey(number))
            {
                var correspondingSet = Map[number];
                if (results.Count == 0)
                {
                    results = new List<string>(correspondingSet);
                }
                else
                {
                    var temp = new List<string>(results);
                    results = new List<string>();

                    foreach (var c in correspondingSet)
                    {
                        foreach (var t in temp)
                        {
                            results.Add(t + c);
                        }
                    }
                }
                
            }
            else
            {
                Console.WriteLine($"Unable to find key: {d}");
            }
        }

        return results;
    }
}