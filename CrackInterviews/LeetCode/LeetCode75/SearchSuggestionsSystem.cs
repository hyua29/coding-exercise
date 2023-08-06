namespace LeetCode.LeetCode75;

public class SearchSuggestionsSystem
{
    public IList<IList<string>> SuggestedProducts(string[] products, string searchWord)
    {
        Array.Sort(products);

        var results = new List<IList<string>>();

        var root = new Trie();

        foreach (var product in products)
        {
            var node = root;
            foreach (var c in product)
            {
                var index = c - 'a';
                node.Next[index] ??= new Trie();

                var current = node.Next[index]!;
                if (current.Words.Count < 3)
                {
                    current.Words.Add(product);
                }

                node = current;
            }
        }

        var trie = root;
        foreach (var c in searchWord)
        {
            var index = c - 'a';

            trie.Next[index] ??= new Trie();

            results.Add(trie.Next[index]!.Words);
            trie = trie.Next[index]!;
        }

        return results;
    }


    //trie node
    private class Trie
    {
        public Trie?[] Next { get; }
        public List<string> Words { get; }

        public Trie()
        {
            Words = new List<string>();
            Next = new Trie[26];
        }
    }
}