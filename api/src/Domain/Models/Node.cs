using System.Collections.Generic;
using System.Linq;

namespace SuggestionApi.Domain.Models
{
    public class Node
    {
        public char Value { get; set; }
        public List<Node> Children { get; set; }
        public Node Parent { get; set; }
        public int Depth { get; set; }
        
        // Rider suggests .Any() but Count() is quicker since it 
        // is directly implemented in List<T>
        public bool IsLeaf => Children.Count() == 0;

        public Node(char value, int depth, Node parent)
        {
            Value = value;
            Children = new List<Node>();
            Depth = depth;
            Parent = parent;
        }

        public Node FindChildNode(char c)
        {
            foreach (var child in Children)
                if (child.Value == c)
                    return child;

            return null;
        }
    }
}