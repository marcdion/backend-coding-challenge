using System.Collections.Generic;
using SuggestionApi.Domain.Models.Locations;
using SuggestionApi.Domain.Models.Suggestions;

namespace SuggestionApi.Domain.Models.DataStructure
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

        public void Insert(Location val, double? avg)
        {
            var commonPrefix = Prefix(val.Name);
            var current = commonPrefix;

            for (var i = current.Depth; i < val.Name.Length; i++)
            {
                var newNode = new Node(val.Name[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }
            
            current.Locations.Add(new LocationLean(current, val.Latitude, val.Longitude, val.Country, val.AdministrativeRegion, CalculateBaseScore(val.Population, avg)));;
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
                        Name = $"{new string(chars)}, {locationNode.AdministrativeRegion}, {locationNode.Country}",
                        Latitude = locationNode.Latitude,
                        Longitude = locationNode.Longitude,
                        BaseScore = locationNode.BaseScore,
                        DepthDifference = wordNode.Depth - commonPrefix.Depth,
                        Popularity = wordNode.Popularity
                    });
                }

                wordNode.Popularity++;
            }
            
            return results;
        }

        private void FindAllChildWords(Node n, ICollection<Node> wordNodes)
        {
            if(n.IsLeaf())
                wordNodes.Add(n);

            foreach (var node in n.Children)
                FindAllChildWords(node, wordNodes);
        }

        private double CalculateBaseScore(double? population, double? ceiling)
        {
            return !population.HasValue || !ceiling.HasValue ? 0 : population.Value / ceiling.Value;
        }
    }
}