namespace LeetCode.Atlassian;

using System.Diagnostics;

public class RankTeamsByVotes
{
    // TODO: Check this answer for improvement - https://leetcode.com/problems/rank-teams-by-votes/solutions/524853/java-o-26n-26-2-log26-sort-by-high-rank-vote-to-low-rank-vote/?orderBy=most_votes
    public string RankTeams(string[] votes)
    {
        Debug.Assert(votes != null);

        var cache = new List<Dictionary<char, int>>(votes[0].Length);
        foreach (var c in votes[0])
        {
            var dict = new Dictionary<char, int>();
            cache.Add(dict);

            foreach (var cc in votes[0])
            {
                dict[cc] = 0;
            }
        }

        for (int i = 0; i < votes[0].Length; i++)
        {
            for (int j = 0; j < votes.Length; j++)
            {
                var c = votes[j][i];

                cache[i][c]++;
            }
        }

        var result = votes[0].OrderBy(c => c).OrderByDescending(c => cache[0][c]);

        for (int i = 1; i < cache.Count; i++)
        {
            var index = i;
            result = result.ThenByDescending(c => cache[index][c]);
        }

        return string.Join("", result.ToArray());
    }

    [TestFixture]
    public class RankTeamsTests
    {
        [TestCaseSource(nameof(GetTestCaseData))]
        public void RankTeamsTest(string[] inputs, string expected)
        {
            var result = new RankTeamsByVotes().RankTeams(inputs);

            Assert.That(result, Is.EqualTo(expected));
        }

        public static IEnumerable<TestCaseData> GetTestCaseData()
        {
            yield return new TestCaseData(new[] {"ABC", "ACB", "ABC", "ACB", "ACB"}, "ACB");
            yield return new TestCaseData(new[] {"BCA", "CAB", "CBA", "ABC", "ACB", "BAC"}, "ABC");
            yield return new TestCaseData(new[] {"WXYZ", "XYZW"}, "XWYZ");
            yield return new TestCaseData(new[] {"ZMNAGUEDSJYLBOPHRQICWFXTVK"}, "ZMNAGUEDSJYLBOPHRQICWFXTVK");
        }
    }
}