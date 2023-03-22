using System.Diagnostics;

namespace LeetCode;

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

            Console.WriteLine($"The leader at {times[i]} is {_leaderCache[i]}");
        }
    }

    public int Q(int t)
    {
        var index = Array.BinarySearch(_timeCache, t);
        Console.WriteLine($"index is: {index} {~index}");
        return index >= 0 ? _leaderCache[index] : _leaderCache[~index - 1];
    }
}