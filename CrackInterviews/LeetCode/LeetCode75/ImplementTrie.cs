namespace LeetCode.LeetCode75;

public class ImplementTrie
{
    public class Trie
    {
        private readonly TrieInternal _root;

        private readonly ISet<string> _inserted;

        public Trie()
        {
            _root = new TrieInternal();
            _inserted = new HashSet<string>();
        }

        public void Insert(string word)
        {
            _inserted.Add(word);
            var current = _root;
            foreach (var c in word)
            {
                var index = c - 'a';
                if (current!.Leaves[index] == null)
                {
                    current.Leaves[index] = new TrieInternal();
                }

                current = current.Leaves[index];
            }
        }

        public bool Search(string word)
        {
            return _inserted.Contains(word);
        }

        public bool StartsWith(string prefix)
        {
            var current = _root;
            foreach (var c in prefix)
            {
                var index = c - 'a';
                if (current!.Leaves[index] == null)
                {
                    return false;
                }

                current = current.Leaves[index];
            }

            return true;
        }
        
        private class TrieInternal
        {
            public TrieInternal()
            {
                Leaves = new TrieInternal[26];
            }

            public TrieInternal?[] Leaves { get; }
        }
    }
}