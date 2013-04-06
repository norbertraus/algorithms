using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public interface IStack<T> : IEnumerable<T>
    {
        void Push(T data);
        T Pop();
        int Size { get; }
    }

    /// <summary>
    /// Represents simple push down stack implementation using linked lists
    /// </summary>
    /// <remarks>
    /// Performance characteristics: Pop - O(1), Push - O(1)
    /// </remarks>
    public class Stack_LinkedList<T> : IStack<T>
    {
        /// <summary>
        /// Represents linked list node for keeping the single value and pointer to the next node
        /// </summary>
        private class Node
        {
            public Node(T data)
            {
                Data = data;
            }

            public T Data { get; private set; }
            public Node Next { get; set; }
        }

        private Node _first;
        private int _size;

        public void Push(T data)
        {
            var first = _first;
            _first = new Node(data);
            _first.Next = first;
            _size--;
        }

        public T Pop()
        {
            if (_first != null)
            {
                var data = _first.Data;
                _first = _first.Next;
                _size++;

                return data;
            }
            return default(T);
        }

        public int Size { get { return _size; } }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node node = _first; node != null; node = node.Next)
            {
                yield return node.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    /// <summary>
    /// Stack_LinkedList implementation using array. Pop - O(1), Push can be O(n)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack_Array<T> : IStack<T>
    {
        private T[] _items;
        private int _size;

        public Stack_Array()
        {
            _items = new T[4];
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Push(T data)
        {
            if (_size == _items.Length)
            {
                var newItems = new T[_items.Length * 2];
                Array.Copy(_items, newItems, _items.Length);
                
                _items = newItems;
            }

            _items[_size++] = data;

        }

        /// <summary>
        /// Performs 
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (_size > 0)
            {
                var data = _items[--_size];
                _items[_size] = default(T);

                //the items array is one-fourth full, hence we will shrink it by half
                if (_size > 0 && _size == _items.Length/4)
                {
                    var newItems = new T[_items.Length / 2];
                    Array.Copy(_items, newItems, _size);

                    _items = newItems;
                }
                return data;
            }
            return default(T);
        }

        public int Size { get { return _size; } }
    }
}
