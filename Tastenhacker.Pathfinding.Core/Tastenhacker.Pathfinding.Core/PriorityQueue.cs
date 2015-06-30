//----------------------------------------------
//              ~ DejaVu Graph Library ~
//                © 2013 Thomas Hummes
//        based upon work of the GC1011 class
//----------------------------------------------

using System.Collections.Generic;

namespace Tastenhacker.Pathfinding.Core
{
    public interface IKeyDecreaser<in T>
    {
        void decreaseKey(T item, int amount = -1);
    }

    public class PriorityQueue<T> : IList<T>
    {
        private readonly List<T> _items = new List<T>();
        private readonly IComparer<T> _comparer;
        private readonly IKeyDecreaser<T> _decreaser;

        public PriorityQueue(IComparer<T> comparer, IKeyDecreaser<T> decreaser)
        {
            this._comparer = comparer;
            this._decreaser = decreaser;
        }

        public void Insert(T item)
        {
            _items.Add(item);
            _items.Sort(_comparer);            
        }

        public T AccessMin()
        {
            return _items.Count == 0 ? default(T) : _items[0];
        }

        public T ExtractMin()
        {
            if (_items.Count == 0)
                return default(T);
            T minItem = _items[0];
            _items.Remove(minItem);
            return minItem;
        }

        public void DecreaseKey(T item, int amount = -1)
        {
            _decreaser.decreaseKey(item, amount);
        }

        public void Merge(IList<T> source)
        {
            foreach (T item in source)
            {
                if (!_items.Contains(item))
                {
                    Insert(item);
                }
            }
            _items.Sort(_comparer);
        }

        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            _items.Insert(index, item);
            _items.Sort(_comparer);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return _items[index];
            }
            set
            {
                _items[index] = value;
                _items.Sort(_comparer);
            }
        }

        public void Add(T item)
        {
            Insert(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
            _items.Sort(_comparer);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }

}
