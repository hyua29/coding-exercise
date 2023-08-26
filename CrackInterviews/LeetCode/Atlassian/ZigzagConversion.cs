namespace LeetCode.Atlassian;

using System.Text;

public class ZigzagConversion
{
    public string Convert(string s, int numRows)
    {
        if (numRows == 1) return s;

        var arraySize = 2 * numRows - 2;

        var lists = new List<string>();
        for (int i = 0; i < numRows; i++)
        {
            lists.Add(string.Empty);
        }

        var multiplier = 0;
        for (int j = 1; j <= s.Length; j++)
        {
            var currentIndex = j - multiplier * arraySize;
            if (currentIndex < numRows) lists[currentIndex - 1] += (s[j - 1]);
            else lists[numRows - 1 - currentIndex % numRows] += (s[j - 1]);

            if (j % arraySize == 0)
            {
                multiplier++;
            }
        }

        StringBuilder sb = new StringBuilder();
        foreach (var l in lists)
        {
            sb.Append(l);
        }

        return sb.ToString();
    }
}

public class ZigzagConversionTests
{
    private ZigzagConversion _solution = new ZigzagConversion();

    [Test]
    public void Test()
    {
        Assert.That(_solution.Convert("PAYPALISHIRING", 3), Is.EqualTo("PAHNAPLSIIGYIR"));
    }
}