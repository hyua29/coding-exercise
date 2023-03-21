using System.Diagnostics;

namespace LeetCode;

public class RankTeamsByVotes
{
    // TODO: Check this answer for improvement - https://leetcode.com/problems/rank-teams-by-votes/solutions/524853/java-o-26n-26-2-log26-sort-by-high-rank-vote-to-low-rank-vote/?orderBy=most_votes
    public string RankTeams(string[] votes)
    {
        Debug.Assert(votes != null);

        if (votes.Length == 0 || votes[0].Length == 0)
        {
            return string.Empty;
        }

        var numOfTeams = votes[0].Length;

        var teamOrder = new char[numOfTeams];
        var teamNotArranged = new HashSet<char>(votes[0].Select(x => x));

        for (int i = 0; i < teamOrder.Length; i++)
        {
            var chosenTeam = DetermineTeamOrder(votes, new HashSet<char>(teamNotArranged));

            teamOrder[i] = chosenTeam;
            teamNotArranged.Remove(chosenTeam);
        }

        return string.Join("", teamOrder);
    }

    private static char DetermineTeamOrder(string[] votes, HashSet<char> candidates)
    {
        Debug.Assert(votes?.Length > 0);
        Debug.Assert(candidates != null);

        for (int i = 0; i < votes[0].Length; i++)
        {
            var buffer = new int[26];

            foreach (var v in votes)
            {
                if (candidates.Contains(v[i]))
                {
                    buffer[v[i] - 'A']++;
                }
            }

            NarrowCandidates(buffer, candidates);

            if (candidates.Count == 1)
            {
                return candidates.First();
            }
        }

        // Teams are still tied after considering all positions, we rank them alphabetically based on their team letter.
        return candidates.OrderBy(c => c).First();
    }

    private static void NarrowCandidates(int[] buffer, HashSet<char> candidates)
    {
        Debug.Assert(buffer != null);
        Debug.Assert(buffer.Length == 26);

        var maxCount = buffer.Max();

        // Nothing is in the buffer, we can't narrow down the candidates
        if (maxCount == 0)
        {
            return;
        }

        for (int i = 0; i < buffer.Length; i++)
        {
            var count = buffer[i];

            if (count != maxCount)
            {
                candidates.Remove((char) ('A' + i));
            }
        }
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
            yield return new TestCaseData(new[] {"WXYZ", "XYZW"}, "XWYZ");
            yield return new TestCaseData(new[] {"ZMNAGUEDSJYLBOPHRQICWFXTVK"}, "ZMNAGUEDSJYLBOPHRQICWFXTVK");
        }
    }
}