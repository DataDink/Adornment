using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Adorners.Extensions
{
    public class DependencyCollection<T> : DependencyObject, IList, IList<T>, INotifyCollectionChanged
    {
        private List<T> Internal { get; set; }

        public DependencyCollection()
        {
            Internal = new List<T>();
        }

        public void Add(T item)
        {
            Internal.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void AddRange(IEnumerable<T> items)
        {
            var list = items.ToList();
            Internal.AddRange(list);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list));
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return Internal.AsReadOnly();
        }

        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return Internal.BinarySearch(item, comparer);
        }

        public int BinarySearch(T item)
        {
            return Internal.BinarySearch(item);
        }

        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return Internal.BinarySearch(index, count, item, comparer);
        }

        public void Clear()
        {
            var oldItems = Internal.ToArray();
            Internal.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems));
        }

        public bool Contains(T item)
        {
            return Internal.Contains(item);
        }

        public DependencyCollection<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            var intl = Internal.ConvertAll(converter);
            var obsv = new DependencyCollection<TOutput>() { Internal = intl };
            return obsv;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Internal.CopyTo(array, arrayIndex);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            Internal.CopyTo(index, array, arrayIndex, count);
        }

        public void CopyTo(T[] array)
        {
            Internal.CopyTo(array);
        }

        public bool Exists(Predicate<T> match)
        {
            return Internal.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return Internal.Find(match);
        }

        public DependencyCollection<T> FindAll(Predicate<T> match)
        {
            var intl = Internal.FindAll(match);
            var obsv = new DependencyCollection<T> { Internal = intl };
            return obsv;
        }

        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return Internal.FindIndex(startIndex, count, match);
        }

        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return Internal.FindIndex(startIndex, match);
        }

        public int FindIndex(Predicate<T> match)
        {
            return Internal.FindIndex(match);
        }

        public T FindLast(Predicate<T> match)
        {
            return Internal.FindLast(match);
        }

        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return Internal.FindLastIndex(startIndex, count, match);
        }

        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return Internal.FindLastIndex(startIndex, match);
        }

        public int FindLastIndex(Predicate<T> match)
        {
            return Internal.FindLastIndex(match);
        }

        public void ForEach(Action<T> action)
        {
            Internal.ForEach(action);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        public DependencyCollection<T> GetRange(int index, int count)
        {
            var intl = Internal.GetRange(index, count);
            var obsv = new DependencyCollection<T> { Internal = intl };
            return obsv;
        }

        public int IndexOf(T item, int index, int count)
        {
            return Internal.IndexOf(item, index, count);
        }

        public int IndexOf(T item, int index)
        {
            return Internal.IndexOf(item, index);
        }

        public int IndexOf(T item)
        {
            return Internal.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Internal.Insert(index, item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            var list = collection.ToList();
            Internal.InsertRange(index, list);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, list, index));
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return Internal.LastIndexOf(item, index, count);
        }

        public int LastIndexOf(T item, int index)
        {
            return Internal.LastIndexOf(item, index);
        }

        public int LastIndexOf(T item)
        {
            return Internal.LastIndexOf(item);
        }

        public bool Remove(T item)
        {
            var success = Internal.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            return success;
        }

        public int RemoveAll(Predicate<T> match)
        {
            var removed = Internal.FindAll(match); // ouch - I wonder if there is some better way to trap what has been removed
            var count = Internal.RemoveAll(match);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed));
            return count;
        }

        public void RemoveAt(int index)
        {
            var removed = Internal[index];
            Internal.RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed, index));
        }

        public void RemoveRange(int index, int count)
        {
            var removed = Internal.GetRange(index, count);
            Internal.RemoveRange(index, count);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removed, index));
        }

        public void Reverse(int index, int count)
        {
            var previous = Internal.GetRange(index, count);
            Internal.Reverse(index, count);
            var current = Internal.GetRange(index, count);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, current, previous, index));
        }

        public void Reverse()
        {
            var previous = Internal.ToList();
            Internal.Reverse();
            var current = Internal.ToList();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, current, previous, 0));
        }

        public void Sort(Comparison<T> comparison)
        {
            var previous = Internal.ToList();
            Internal.Sort(comparison);
            var current = Internal.ToList();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, current, previous, 0));
        }

        public void Sort(int index, int count, IComparer<T> comparison)
        {
            var previous = Internal.GetRange(index, count);
            Internal.Sort(index, count, comparison);
            var current = Internal.GetRange(index, count);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, current, previous, index));
        }

        public void Sort(IComparer<T> comparison)
        {
            var previous = Internal.ToList();
            Internal.Sort(comparison);
            var current = Internal.ToList();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, current, previous, 0));
        }

        public void Sort()
        {
            var previous = Internal.ToList();
            Internal.Sort();
            var current = Internal.ToList();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, current, previous, 0));
        }

        public void TrimExcess()
        {
            Internal.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return Internal.TrueForAll(match);
        }

        public int Capacity { get { return Internal.Capacity; } set { Internal.Capacity = value; } }

        public int Count { get { return Internal.Count; } }

        public T this[int index]
        {
            get { return Internal[index]; }
            set
            {
                var previous = Internal[index];
                Internal[index] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, previous, index));
            }
        }

        #region Observable
        private void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, args);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region IList
        bool IList.IsReadOnly { get { return ((IList) Internal).IsReadOnly; } }
        bool IList.IsFixedSize { get { return ((IList) Internal).IsFixedSize; } }
        void IList.RemoveAt(int index)
        {
            var item = Internal[index];
            ((IList)Internal).RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }
        object IList.this[int index]
        {
            get { return ((IList)Internal)[index]; }
            set
            {
                var previous = Internal[index];
                ((IList)Internal)[index] = value;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, previous, index));
            }
        }
        void IList<T>.RemoveAt(int index)
        {
            var item = Internal[index];
            ((IList<T>)Internal).RemoveAt(index);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
        }
        void IList.Clear()
        {
            var oldItems = Internal.ToArray();
            ((IList)Internal).Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems));
        }
        int IList.IndexOf(object value)
        {
            return ((IList)Internal).IndexOf(value);
        }
        void IList.Insert(int index, object value)
        {
            ((IList)Internal).Insert(index, value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));
        }
        void IList.Remove(object value)
        {
            ((IList)Internal).Remove(value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value));
        }
        int IList.Add(object value)
        {
            var result = ((IList)Internal).Add(value);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value));
            return result;
        }
        bool IList.Contains(object value)
        {
            return ((IList)Internal).Contains(value);
        }
        #endregion

        #region ICollection
        int ICollection<T>.Count { get { return ((ICollection<T>)Internal).Count; } }
        bool ICollection<T>.IsReadOnly { get { return ((ICollection<T>)Internal).IsReadOnly; } }
        int ICollection.Count { get { return ((ICollection)Internal).Count; } }
        object ICollection.SyncRoot { get { return ((ICollection)Internal).SyncRoot; } }
        bool ICollection.IsSynchronized { get { return ((ICollection)Internal).IsSynchronized; } }
        void ICollection<T>.Clear()
        {
            var oldItems = Internal.ToArray();
            ((ICollection<T>)Internal).Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems));
        }
        void ICollection.CopyTo(Array array, int index)
        {
            ((IList)Internal).CopyTo(array, index);
        }
        #endregion
    }
}
