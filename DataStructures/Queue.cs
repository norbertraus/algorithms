using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
    public interface IQueue<T> : IEnumerable<T>
    {
        void Enqueue(T data);
        T Dequeue();
        int Size { get; }
        bool IsEmpty { get; }
    }

    public class Queue_LinkedList<T> : IQueue<T>
    {
        private class Node
        {
            public Node(T data)
            {
                Data = data;
            }

            public T Data { get; set; }
            public Node Next { get; set; }
        }

        private Node _first;
        private Node _last;
        private int _size;

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

        public void Enqueue(T data)
        {
            var node = new Node(data);
            _size++;

            if (IsEmpty)
            {
                _last = node;
                _first = _last;
            }
            else
            {
                _last.Next = node;
                _last = node;
            }
        }

        public T Dequeue()
        {
            if (_first == null)
            {
                return default(T);
            }
            
            var node = _first;
            _first = _first.Next;

            if (IsEmpty)
            {
                _last = null;
            }
            
            _size--;
            return node.Data;
        }

        public int Size { get { return _size; } }

        public bool IsEmpty { get { return _first == null; } }
    }

    /// <summary>
    /// Represents queue implemented as an cicular buffer
    /// </summary>
    public class Queue_Array<T> : IQueue<T>
    {
        private T[] _items = new T[4];
        private int _head; //points to the first elements in the array
        private int _tail; //points to the next free bucket in the array
        private int _size;
        
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                var index = (_head + i) % Capacity; //works out the index in the circular buffer taking into account the fact that tail < head
                yield return _items[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enqueue(T data)
        {
            if (Size == _items.Length)
            {
                var newItems = new T[_items.Length*2];
                if (_head < _tail)
                {
                    //copy items starting from head up to tail
                    Array.Copy(_items, _head, newItems, 0, _size);
                }
                else
                {
                    //copy item from head till end of buffer
                    Array.Copy(_items, _head, newItems, 0, _items.Length - _head);
                    //copy items from the beginning up to tail
                    Array.Copy(_items, 0, newItems, _items.Length - _head, _tail);
                }
                _items = newItems;
                _head = 0;
                _tail = _size;
            }

            _items[_tail] = data;
            _tail = (_tail + 1) % Capacity;
            _size++;
        }

        public T Dequeue()
        {
            if (_head > 0)
            {
                var item = _items[_head];
                _items[_head] = default(T);
                _head = (_head + 1) % Capacity;
                _size--;

                return item;
            }
            return default(T);
        }

        public int Size { get { return _size; } }

        public bool IsEmpty { get { return _size == 0; } }

        internal int Capacity { get { return _items.Length; } }
    }
}