using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Trie<T>
    {
        public const int AlphabetSize = 256;

        private Node _root = new Node(); //root node of the trie

        public class Node
        {
            private Node[] _links = new Node[AlphabetSize];

            public T Value { get; set; }
            public Node[] Links { get { return _links; } }
        }

        public T GetValue(string key)
        {
            Node match = Get(_root, key);
            if(match != null)
            {
                return match.Value;
            }
            return default(T);
        }

        public void Put(string key, T value)
        {
            Put(_root, key, value);
        }

        /// <summary>
        /// Provides the number of keys in <see cref="Trie{T}"/>
        /// </summary>
        public int Size
        {
            get { return GetSize(_root); }
        }

        public Node Root { get { return _root; } }

        private int GetSize(Node node)
        {
            if(node == null) return 0;
            int size = 0;

            if(!Equals(node.Value, default(T))) size++;
            for(int next = 0; next < AlphabetSize; next++)
            {
                size += GetSize(node.Links[next]);
            }

            return size;
        }

        /// <summary>
        /// Provides all keys available in <see cref="Trie{T}"/>
        /// </summary>
        public IEnumerable<string> Keys
        {
            get { return KeysWithPrefix(string.Empty); }
        }

        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            return Collect(Get(_root, prefix), prefix);
        }

        public IEnumerable<string> KeysWithMatch(string pattern)
        {
            return Collect(_root, "", pattern);
        }

        public string LongestPrefixOf(string prefix)
        {
            var length = Search(_root, prefix);
            return prefix.Substring(0, length);
        }

        public void Delete(string key)
        {
            _root = Delete(_root, key, 0);
        }

        private Node Delete(Node node, string key, int d)
        {
            if(node == null) return null;
            if(d == key.Length)
                node.Value = default(T);
            else
            {
                var @char = key[d];
                node.Links[@char] = Delete(node.Links[@char], key, d + 1);
            }

            if(!Equals(node.Value, default(T))) return node;

            for(int next = 0; next < AlphabetSize; next++)
            {
                if(node.Links[next] != null)
                    return node;
            }
            return null;
        }

        private int Search(Node node, string prefix, int current = 0, int length = 0)
        {
            if(node == null) return length;
            if(!Equals(node.Value, default(T))) length = current;
            if(current == prefix.Length) return length;

            var @char = prefix[current];
            return Search(node.Links[@char], prefix, current + 1, length);
        }

        private IEnumerable<string> Collect(Node root, string prefix)
        {
            if(root == null) yield break;
            if(!Equals(root.Value, default(T)))
                yield return prefix;

            for(int next = 0; next < AlphabetSize; next++)
            {
                foreach(var key in Collect(root.Links[next], prefix + (char)next))
                {
                    yield return key;
                }
            }

        }

        private IEnumerable<string> Collect(Node root, string prefix, string pattern)
        {
            if(root == null) yield break;
            if(pattern.Length == prefix.Length && !Equals(root.Value, default(T)))
                yield return prefix;
            if(pattern.Length == prefix.Length) yield break;

            var @char = pattern[prefix.Length];
            for(int next = 0; next < AlphabetSize; next++)
            {
                if(@char == '*' || @char == (char)next)
                {
                    foreach(var key in Collect(root.Links[next], prefix + (char)next, pattern))
                    {
                        yield return key;
                    }
                }
            }
        }

        private Node Put(Node node, string key, T value, int position = 0)
        {
            if(node == null) node = new Node();
            if(key.Length == position)
            {
                node.Value = value;
                return node;
            }
            var @char = key[position];
            node.Links[@char] = Put(node.Links[@char], key, value, position + 1);
            return node;
        }

        private Node Get(Node node, string key, int position = 0)
        {
            if(node == null) return null;
            if(key.Length == position) return node;

            //get character at position
            var @char = key[position];
            return Get(node.Links[@char], key, position + 1);
        }
    }
}
