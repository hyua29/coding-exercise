namespace LeetCode.Atlassian;

public class GroupAnagrams
{
    public IList<IList<string>> RunGroupAnagrams(string[] strs)
    {
        var dic = new Dictionary<string, IList<string>>();

        foreach (var s in strs)
        {
            var ss = string.Join("", s.OrderBy(c => c));
            if (dic.ContainsKey(ss))
                dic[ss].Add(s);
            else
                dic.Add(ss, new List<string> {s});
        }

        return dic.Select(p => p.Value).OrderBy(v => v.Count).ToList();
    }
}