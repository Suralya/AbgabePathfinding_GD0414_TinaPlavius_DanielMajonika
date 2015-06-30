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
        private readonly List<T> items = new List<T>();
        private readonly IComparer<T> comparer;
        private readonly IKeyDecreaser<T> decreaser;

        public PriorityQueue(IComparer<T> comparer, IKeyDecreaser<T> decreaser)
        {
            this.comparer = comparer;
            this.decreaser = decreaser;
        }

        public void insert(T item)
        {
            items.Add(item);
            items.Sort(comparer);            
        }

        public T accessMin()
        {
            return items.Count == 0 ? default(T) : items[0];
        }

        public T extractMin()
        {
            if (items.Count == 0)
                return default(T);
            T minItem = items[0];
            items.Remove(minItem);
            return minItem;
        }

        public void decreaseKey(T item, int amount = -1)
        {
            decreaser.decreaseKey(item, amount);
        }

        public bool remove(T item)
        {
            return items.Remove(item);
        }

        public void merge(IList<T> source)
        {
            foreach (T item in source)
            {
                if (!items.Contains(item))
                {
                    insert(item);
                }
            }
            items.Sort(comparer);
        }

        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            items.Insert(index, item);
            items.Sort(comparer);
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public T this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
                items.Sort(comparer);
            }
        }

        public void Add(T item)
        {
            insert(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
            items.Sort(comparer);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }

}
