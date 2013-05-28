using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Adorners.Extensions
{
    public class DependencyCollection<T> : DependencyObject, IList<T>
    {
        protected readonly List<T> Internal = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            Internal.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            Internal.AddRange(items);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public bool Contains(T item)
        {
            return Internal.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Internal.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return Internal.Remove(item);
        }

        public void RemoveRange(int index, int count)
        {
            Internal.RemoveRange(index, count);
        }

        public int Count { get { return Internal.Count; } }
        bool ICollection<T>.IsReadOnly { get { return ((ICollection<T>)Internal).IsReadOnly; } }
        public int IndexOf(T item)
        {
            return Internal.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Internal.Insert(index, item);
        }

        public void InsertRange(int index, IEnumerable<T> items)
        {
            Internal.InsertRange(index, items);
        }

        public void RemoveAt(int index)
        {
            Internal.RemoveAt(index);
        }

        public T this[int index] { get { return Internal[index]; } set { Internal[index] = value; } }
    }
}
