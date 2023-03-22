namespace LeetCode;

using System.Diagnostics;

public class OnlineElection
{
    private readonly int[] _leaderCache;
    private readonly int[] _timeCache;

    public OnlineElection(int[] persons, int[] times)
    {
        Debug.Assert(persons?.Length >= 0);
        Debug.Assert(times?.Length >= 0);
        Debug.Assert(persons.Length == times.Length);

        _leaderCache = new int[times.Length];
        _timeCache = times;

        // Key is the candidate and value is the corresponding votes
        var candidatesToVotes = new Dictionary<int, int>();
        var leader = -1;

        for (int i = 0; i < persons.Length; i++)
        {
            var p = persons[i];
            if (!candidatesToVotes.TryAdd(p, 1))
            {
                candidatesToVotes[p]++;
            }

            candidatesToVotes.TryGetValue(leader, out int oldLeaderVotes);

            if (candidatesToVotes[p] >= oldLeaderVotes)
            {
                leader = p;
            }

            _leaderCache[i] = leader;
        }
    }

    public int Q(int t)
    {
        var index = Array.BinarySearch(_timeCache, t);
        return index >= 0 ? _leaderCache[index] : _leaderCache[~index - 1];
    }

    [TestFixture]
    public class OnlineElectionTests
    {
        [TestCaseSource(nameof(GetTestCaseDate))]
        public void OnlineElectionTest(int[] persons, int[] times, int[] query, int[] expectedResults)
        {
            var e = new OnlineElection(persons, times);

            for (int i = 0; i < query.Length; i++)
            {
                Assert.That(e.Q(query[i]), Is.EqualTo(expectedResults[i]));
            }
        }

        private static IEnumerable<TestCaseData> GetTestCaseDate()
        {
            yield return new TestCaseData(new int[] {0, 1, 1, 0, 0, 1, 0}, new int[] {0, 5, 10, 15, 20, 25, 30},
                new int[] {3, 12, 25, 15, 24, 8}, new int[] {0, 1, 1, 0, 0, 1});

            yield return new TestCaseData(new int[] {0, 1, 0, 1, 1}, new int[] {24, 29, 31, 76, 81},
                new int[] {28, 24, 29, 77, 30, 25, 76, 75, 81, 80}, new int[] {0, 0, 1, 1, 1, 0, 1, 0, 1, 1});
        }
    }
}