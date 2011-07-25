﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ComponentModel;
using Inscribe.Data;
using Livet;
using System.Threading.Tasks;
using System.Threading;

namespace Inscribe.Data
{
    public class CachedConcurrentObservableCollection<T>
        : INotifyCollectionChanged, ICollection<T>
    {
        #region Internal storage

        private T[] BehindArray;

        private SafeLinkedList<T> BehindCollection;

        #endregion

        public CachedConcurrentObservableCollection()
        {
            this.BehindArray = new T[0];
            this.BehindCollection = new SafeLinkedList<T>();
        }

        /// <summary>
        /// コレクションの変更をキャッシュに反映します。<para />
        /// ディスパッチャスレッドから実行した場合、ノンブロック実行します。
        /// </summary>
        /// <param name="invalidateCollection">コレクションが変更されたことを通知するか</param>
        public void Commit(bool invalidateCollection = false)
        {
            if (DispatcherHelper.UIDispatcher.CheckAccess())
            {
                Task.Factory.StartNew(() => Commit(invalidateCollection));
                return;
            }
            var array = BehindCollection.ToArray();
            Interlocked.Exchange(ref BehindArray, array);
            if (invalidateCollection)
                RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Add(T item)
        {
            this.BehindCollection.AddLast(item);
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public void Clear()
        {
            this.BehindCollection.Clear();
            this.Commit(true);
        }

        public bool Contains(T item)
        {
            return this.BehindCollection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.BehindArray.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.BehindCollection.Count; }
        }

        public bool Remove(T item)
        {
            var idx = Array.IndexOf(this.BehindCollection.ToArray(), item);
            var ret = this.BehindCollection.Remove(item);
            if(idx  >= 0)
                RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, idx));
            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.BehindArray.Cast<T>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = this.CollectionChanged;
            if (handler != null)
                DispatcherHelper.BeginInvoke(() => handler(this, e));
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return true; }
        }
    }
}
