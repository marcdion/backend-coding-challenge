namespace SuggestionApi.Domain.Models
{
    public class SharedTrie
    {
        private readonly Trie _trie
            = new Trie();

        public Trie Trie => _trie;
    }
}