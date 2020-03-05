using System;
using System.Collections.Generic;

namespace SuggestionApi.Domain.Models
{
    //See ADR-003 for why I chose a Trie data structure
    public class Trie
    {
        private readonly Node _root;

        public Trie()
        {
            _root = new Node('^', 0, null);
        }

        public Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        public void Insert(GeoName val)
        {
            var commonPrefix = Prefix(val.Name);
            var current = commonPrefix;

            for (var i = current.Depth; i < val.Name.Length; i++)
            {
                var newNode = new Node(val.Name[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }
        }

        public IEnumerable<string> GetSuggestionsForPrefix(string s)
        {
            var results = new List<string>();
            var wordNodes = new List<Node>();
            var commonPrefix = Prefix(s);

            FindAllChildWords(commonPrefix, wordNodes);

            foreach (var wordNode in wordNodes)
            {
                var current = wordNode;
                var chars = new char[wordNode.Depth];
                for (var i = wordNode.Depth - 1; i >= 0; i--)
                {
                    chars[i] = current.Value;
                    current = current.Parent;
                }

                results.Add(new string(chars));
            }
            
            return results;
        }

        private void FindAllChildWords(Node n, ICollection<Node> wordNodes)
        {
            if(n.IsLeaf)
                wordNodes.Add(n);

            foreach (var node in n.Children)
                FindAllChildWords(node, wordNodes);
        }
    }
}