namespace SuggestionApi.Domain.Models.DataStructure
{
    public class SharedTrie
    {
        private readonly Trie _trie
            = new Trie();

        public Trie Trie => _trie;
    }
}