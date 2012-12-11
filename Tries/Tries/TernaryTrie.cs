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

        public void Put(string key, T value)
        {
            _root = Put(_root, key, value, 0);
        }

        private Node Put(Node node, string key, T value, int position)
        {
            var @char = key[position];
            if(node == null)
            {
                node = new Node { Character = @char };
            }
            if(@char < node.Character) node.Left = Put(node.Left, key, value, position);
            else if(@char > node.Character) node.Right = Put(node.Right, key, value, position);
            else if (position < key.Length - 1) node.Middle = Put(node.Middle, key, value, position + 1);
            else node.Value = value;

            return node;
        }

        private Node Get(Node node, string key, int position)
        {
            if(node == null) return null;

            var @char = key[position];
            if(@char < node.Character) return Get(node.Left, key, position);
            if(@char > node.Character) return Get(node.Right, key, position);
            if(position < key.Length - 1) return Get(node.Middle, key, position + 1);

            return node;
        }
    }
}