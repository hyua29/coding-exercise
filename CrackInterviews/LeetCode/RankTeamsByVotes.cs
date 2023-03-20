namespace LeetCode;

using System.Diagnostics;

public class RankTeamsByVotes
{
    public string RankTeams(string[] votes)
    {
        Debug.Assert(votes != null);

        if (votes.Length == 0 || votes[0].Length == 0)
        {
            return string.Empty;
        }

        var numOfTeams = votes[0].Length;

        var teamOrder = new char[numOfTeams];
        var teamsArranged = new HashSet<char>();

        for (int i = 0; i < teamOrder.Length; i++)
        {
            var chosenTeam =  DetermineTeamOrder(i, votes, teamsArranged);

            teamOrder[i] = chosenTeam;
            teamsArranged.Add(chosenTeam);
        }

        return string.Join("", teamOrder);
    }

    private static char DetermineTeamOrder(int position, string[] votes, HashSet<char> teamsArranged)
    {
        var buffer = new int[26];

        foreach (var v in votes)
        {
            buffer[v[position] - 'A']++;
        }

        var candidates = FindCandidates(buffer);

        return candidates.Count == 1 ? candidates.First() : OrderTiedTeam(position + 1, candidates, votes);
    }

    private static HashSet<char> FindCandidates(int[] buffer)
    {
        Debug.Assert(buffer.Length == 26);

        var maxCount = buffer.Max();
        var candidates = new HashSet<char>();

        for (int i = 0; i < buffer.Length; i++)
        {
            var count = buffer[i];

            if (count == maxCount)
            {
                candidates.Add((char) ('A' + i));
            }
        }

        return candidates;
    }

    private static char OrderTiedTeam(int position, HashSet<char> candidates, string[] votes)
    {
        // Teams are still tied after considering all positions, we rank them alphabetically based on their team letter.
        if (position == votes[0].Length)
        {
            return candidates.OrderBy(c => c).First();
        }

        var buffer = new int[26];

        foreach (var v in votes)
        {
            if (candidates.Contains(v[position]))
            {
                buffer[v[position] - 'A']++;
            }
        }

        var newCandidates = FindCandidates(buffer);

        return newCandidates.Count == 1
            ? newCandidates.First()
            : OrderTiedTeam(position + 1, newCandidates, votes);
    }
}