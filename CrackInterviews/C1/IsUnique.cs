namespace C1;

using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
internal class IsUnique
{
    private static bool Calculate1(string input)
    {
        if (input == null)
            return true;

        var set = new HashSet<char>();
        foreach (var i in input)
            if (!set.Contains(i))
                set.Add(i);
            else
                return false;
        return true;
    }

    [TestCase("dsfadsfadsfJJcvoiuz")]
    [TestCase("ds  fadsfadsoiuz")]
    [TestCase("qwewradsfcaxz")]
    public void IsUniqueTest(string input)
    {
        Assert.False(Calculate1(input));
    }

    [TestCase("")]
    public void IsUniqueEmptyTest(string input)
    {
        Assert.True(Calculate1(input));
    }

    [TestCase("qwerasdf")]
    public void IsUniquePassTest(string input)
    {
        Assert.True(Calculate1(input));
    }
}