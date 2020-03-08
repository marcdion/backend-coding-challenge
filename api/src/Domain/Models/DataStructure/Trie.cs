using System.Collections.Generic;
using System.Text.RegularExpressions;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Models.DataStructure
{
    //See ADR-003 for why I chose a Trie data structure
    public class Trie
    {
        private Node _root;

        public Trie()
        {
            _root = new Node('^', 0, null);
        }

        public bool IsEmpty()
        {
            return _root.Children.Count == 0;
        }

        public void Reset()
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

        public void Insert(Location val)
        {
            var sanitizedName = SanitizeInput(val.Name);
            var commonPrefix = Prefix(sanitizedName);
            var current = commonPrefix;

            for (var i = current.Depth; i < sanitizedName.Length; i++)
            {
                var newNode = new Node(sanitizedName[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.IsWord = true;
            current.Locations.Add(new LocationLean(current, val.Latitude, val.Longitude, val.Country, val.AdministrativeRegion, val.Population));;
        }

        public IEnumerable<Suggestion> GetSuggestionsForPrefix(string s)
        {
            var results = new List<Suggestion>();
            var wordNodes = new List<Node>();
            var commonPrefix = Prefix(s);

            FindAllChildWords(commonPrefix, wordNodes);

            foreach (var wordNode in wordNodes)
            {
                var locations = wordNode.Locations;
                var current = wordNode;
                var chars = new char[wordNode.Depth];
                for (var i = wordNode.Depth - 1; i >= 0; i--)
                {
                    chars[i] = current.Value;
                    current = current.Parent;
                }

                foreach (var locationNode in locations)
                {
                    results.Add(new Suggestion
                    {
                        Name = new string(chars),
                        FullName = $"{new string(chars)}, {locationNode.AdministrativeRegion}, {locationNode.Country}",
                        Latitude = locationNode.Latitude,
                        Longitude = locationNode.Longitude,
                        DepthDifference = wordNode.Depth - commonPrefix.Depth,
                        Popularity = wordNode.Popularity,
                        Population = locationNode.Population
                    });
                }

                wordNode.Popularity++;
            }
            
            return results;
        }

        private void FindAllChildWords(Node n, ICollection<Node> wordNodes)
        {
            if(n.IsWord)
                wordNodes.Add(n);

            foreach (var node in n.Children)
                FindAllChildWords(node, wordNodes);
        }

        private string SanitizeInput(string s)
        {
            var specialCharsRemoved = Regex.Replace(s, "[?]", string.Empty);
            return specialCharsRemoved.Trim();
        }
    }
}