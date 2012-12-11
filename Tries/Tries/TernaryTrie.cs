namespace DataStructures
{
    public class TernaryTrie<T>
    {
        public class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Middle { get; set; }

            public char Character { get; set; }
            public T Value { get; set; }
        }

        private Node _root = new Node(); //root node of the trie

        public T GetValue(string key)
        {
            var match = Get(_root, key, 0);
            if(match != null)
            {
                return match.Value;
            }
            return default(T);
        }

        private Node Get(Node node, string key, int position)
        {
            if (node == null) return null;

            var @char = key[position];
            if (@char < node.Character) return Get(node.Left, key, position);
            if (@char > node.Character) return Get(node.Right, key, position);
            if (position < key.Length - 1) return Get(node.Middle, key, position + 1);

            return node;
        }
    }
}