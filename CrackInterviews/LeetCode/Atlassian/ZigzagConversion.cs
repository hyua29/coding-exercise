namespace LeetCode.Atlassian;

using System.Text;

public class ZigzagConversion
{
    public string Convert(string s, int numRows)
    {
        if (numRows == 1) return s;

        var arraySize = 2 * numRows - 2;

        var lists = new List<IList<char>>();
        for (int i = 0; i < numRows; i++)
        {
            lists.Add(new List<char>());
        }

        var multiplier = 0;
        for (int j = 1; j <= s.Length; j++)
        {
            var currentIndex = j - multiplier * arraySize;
            if (currentIndex < numRows) lists[currentIndex - 1].Add(s[j - 1]);
            else lists[numRows - 1 - currentIndex % numRows].Add(s[j - 1]);

            if (j % arraySize == 0)
            {
                multiplier++;
            }
        }

        StringBuilder sb = new StringBuilder();
        foreach (var l in lists)
        {
            var temp = string.Join("", l);
            sb.Append(temp);
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