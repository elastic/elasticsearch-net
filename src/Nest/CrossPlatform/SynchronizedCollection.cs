using System.Runtime.InteropServices;
using Nest;

namespace System.Collections.Generic
{
	// TODO delete this, BulkRequest exposes this as IList preventing us to move to concurrentbag
	[ComVisible(false)]
	public class SynchronizedCollection<T> : IList<T>, IList
	{
		public SynchronizedCollection()
		{
			Items = new List<T>();
			SyncRoot = new Object();
		}

		public SynchronizedCollection(object syncRoot)
		{
			if (syncRoot == null)
				throw new ArgumentNullException("syncRoot");

			Items = new List<T>();
			SyncRoot = syncRoot;
		}

		public SynchronizedCollection(object syncRoot, IEnumerable<T> list)
		{
			if (syncRoot == null)
				throw new ArgumentNullException("syncRoot");
			if (list == null)
				throw new ArgumentNullException("list");

			Items = new List<T>(list);
			SyncRoot = syncRoot;
		}

		public SynchronizedCollection(object syncRoot, params T[] list)
		{
			if (syncRoot == null)
				throw new ArgumentNullException("syncRoot");
			if (list == null)
				throw new ArgumentNullException("list");

			Items = new List<T>(list.Length);
			for (var i = 0; i < list.Length; i++)
				Items.Add(list[i]);

			SyncRoot = syncRoot;
		}

		public int Count
		{
			get
			{
				lock (SyncRoot) return Items.Count;
			}
		}

		public T this[int index]
		{
			get
			{
				lock (SyncRoot) return Items[index];
			}
			set
			{
				lock (SyncRoot)
				{
					if (index < 0 || index >= Items.Count)
						throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

					SetItem(index, value);
				}
			}
		}

		public object SyncRoot { get; }

		protected List<T> Items { get; }

		bool IList.IsFixedSize => false;

		bool ICollection<T>.IsReadOnly => false;

		bool IList.IsReadOnly => false;

		bool ICollection.IsSynchronized => true;

		object IList.this[int index]
		{
			get => this[index];
			set
			{
				VerifyValueType(value);
				this[index] = (T)value;
			}
		}

		object ICollection.SyncRoot => SyncRoot;

		void ICollection.CopyTo(Array array, int index)
		{
			lock (SyncRoot) ((IList)Items).CopyTo(array, index);
		}

		public void Add(T item)
		{
			lock (SyncRoot)
			{
				var index = Items.Count;
				InsertItem(index, item);
			}
		}

		public void Clear()
		{
			lock (SyncRoot) ClearItems();
		}

		public bool Contains(T item)
		{
			lock (SyncRoot) return Items.Contains(item);
		}

		public void CopyTo(T[] array, int index)
		{
			lock (SyncRoot) Items.CopyTo(array, index);
		}

		public bool Remove(T item)
		{
			lock (SyncRoot)
			{
				var index = InternalIndexOf(item);
				if (index < 0)
					return false;

				RemoveItem(index);
				return true;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => ((IList)Items).GetEnumerator();

		public IEnumerator<T> GetEnumerator()
		{
			lock (SyncRoot) return Items.GetEnumerator();
		}

		int IList.Add(object value)
		{
			VerifyValueType(value);

			lock (SyncRoot)
			{
				Add((T)value);
				return Count - 1;
			}
		}

		bool IList.Contains(object value)
		{
			VerifyValueType(value);
			return Contains((T)value);
		}

		int IList.IndexOf(object value)
		{
			VerifyValueType(value);
			return IndexOf((T)value);
		}

		void IList.Insert(int index, object value)
		{
			VerifyValueType(value);
			Insert(index, (T)value);
		}

		void IList.Remove(object value)
		{
			VerifyValueType(value);
			Remove((T)value);
		}

		public int IndexOf(T item)
		{
			lock (SyncRoot) return InternalIndexOf(item);
		}

		public void Insert(int index, T item)
		{
			lock (SyncRoot)
			{
				if (index < 0 || index > Items.Count)
					throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

				InsertItem(index, item);
			}
		}

		public void RemoveAt(int index)
		{
			lock (SyncRoot)
			{
				if (index < 0 || index >= Items.Count)
					throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");


				RemoveItem(index);
			}
		}

		private int InternalIndexOf(T item)
		{
			var count = Items.Count;

			for (var i = 0; i < count; i++)
			{
				if (Equals(Items[i], item)) return i;
			}
			return -1;
		}

		protected virtual void ClearItems() => Items.Clear();

		protected virtual void InsertItem(int index, T item) => Items.Insert(index, item);

		protected virtual void RemoveItem(int index) => Items.RemoveAt(index);

		protected virtual void SetItem(int index, T item) => Items[index] = item;

		private static void VerifyValueType(object value)
		{
			if (value == null)
			{
				if (typeof(T).IsValue()) throw new ArgumentException("value is null and a value type");
			}
			else if (!(value is T))
				throw new ArgumentException($"object is of type {value.GetType().FullName} but collection is of {typeof(T).FullName}");
		}
	}
}
