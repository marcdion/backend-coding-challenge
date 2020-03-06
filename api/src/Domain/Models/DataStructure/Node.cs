using System.Collections.Generic;
using System.Linq;
using SuggestionApi.Domain.Models.Locations;

namespace SuggestionApi.Domain.Models.DataStructure
{
    public class Node
    {
        public char Value { get; set; }
        public List<Node> Children { get; set; }
        public List<LocationLean> Locations { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }
        public int Popularity { get; set; }

        public Node(char value, int depth, Node parent)
        {
            Value = value;
            Children = new List<Node>();
            Locations = new List<LocationLean>();
            Depth = depth;
            Parent = parent;
            Popularity = 0;
        }

        public Node FindChildNode(char c)
        {
            foreach (var child in Children)
                if (char.ToUpperInvariant(child.Value) == char.ToUpperInvariant(c))
                    return child;

            return null;
        }

        public bool IsLeaf()
        {
            // ReSharper suggests .Any() but Count() is quicker since it 
            // is directly implemented in List<T>
            return Children.Count() == 0;
        }
    }
}