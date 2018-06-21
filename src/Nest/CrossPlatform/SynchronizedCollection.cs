using Nest;
// ReSharper disable RemoveRedundantBraces
// ReSharper disable ArrangeMethodOrOperatorBody
// ReSharper disable ArrangeAccessorOwnerBody

namespace System.Collections.Generic
{

	//TODO see if we can get rid of this
	[System.Runtime.InteropServices.ComVisible(false)]
	public class SynchronizedCollection<T> : IList<T>, IList
	{
		private readonly List<T> _items;
		private readonly object _sync;

		public SynchronizedCollection()
		{
			this._items = new List<T>();
			this._sync = new object();
		}

		public SynchronizedCollection(object syncRoot)
		{
			this._sync = syncRoot ?? throw new ArgumentNullException(nameof(syncRoot));
			this._items = new List<T>();
		}

		public SynchronizedCollection(object syncRoot, IEnumerable<T> list)
		{
			if (list == null) throw new ArgumentNullException(nameof(list));

			this._items = new List<T>(list);
			this._sync = syncRoot ?? throw new ArgumentNullException(nameof(syncRoot));
		}

		public SynchronizedCollection(object syncRoot, params T[] list)
		{
			if (syncRoot == null)
				throw new ArgumentNullException(nameof(syncRoot));
			if (list == null)
				throw new ArgumentNullException(nameof(list));

			this._items = new List<T>(list.Length);
			this._items.AddRange(list);

			this._sync = syncRoot;
		}

		public int Count
		{
			get { lock (this._sync) { return this._items.Count; } }
		}

		protected List<T> Items => this._items;

		public object SyncRoot => this._sync;

		public T this[int index]
		{
			get
			{
				lock (this._sync)
				{
					return this._items[index];
				}
			}
			set
			{
				lock (this._sync)
				{
					if (index < 0 || index >= this._items.Count)
						throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {this.Items.Count}");

					this.SetItem(index, value);
				}
			}
		}

		public void Add(T item)
		{
			lock (this._sync)
			{
				var index = this._items.Count;
				this.InsertItem(index, item);
			}
		}

		public void Clear()
		{
			lock (this._sync)
			{
				this.ClearItems();
			}
		}

		public void CopyTo(T[] array, int index)
		{
			lock (this._sync)
			{
				this._items.CopyTo(array, index);
			}
		}

		public bool Contains(T item)
		{
			lock (this._sync)
			{
				return this._items.Contains(item);
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			lock (this._sync)
			{
				return this._items.GetEnumerator();
			}
		}

		public int IndexOf(T item)
		{
			lock (this._sync)
			{
				return this.InternalIndexOf(item);
			}
		}

		public void Insert(int index, T item)
		{
			lock (this._sync)
			{
				if (index < 0 || index > this._items.Count)
						throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {this.Items.Count}");

				this.InsertItem(index, item);
			}
		}

		private int InternalIndexOf(T item)
		{
			var count = _items.Count;

			for (var i = 0; i < count; i++)
			{
				if (object.Equals(_items[i], item))
				{
					return i;
				}
			}
			return -1;
		}

		public bool Remove(T item)
		{
			lock (this._sync)
			{
				var index = this.InternalIndexOf(item);
				if (index < 0)
					return false;

				this.RemoveItem(index);
				return true;
			}
		}

		public void RemoveAt(int index)
		{
			lock (this._sync)
			{
				if (index < 0 || index >= this._items.Count)
					throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {this.Items.Count}");


				this.RemoveItem(index);
			}
		}

		protected virtual void ClearItems()
		{
			this._items.Clear();
		}

		protected virtual void InsertItem(int index, T item)
		{
			this._items.Insert(index, item);
		}

		protected virtual void RemoveItem(int index)
		{
			this._items.RemoveAt(index);
		}

		protected virtual void SetItem(int index, T item)
		{
			this._items[index] = item;
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return false; }
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IList)this._items).GetEnumerator();
		}

		bool ICollection.IsSynchronized => true;

		object ICollection.SyncRoot => this._sync;

		void ICollection.CopyTo(Array array, int index)
		{
			lock (this._sync)
			{
				((IList)this._items).CopyTo(array, index);
			}
		}

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				VerifyValueType(value);
				this[index] = (T)value;
			}
		}

		bool IList.IsReadOnly
		{
			get { return false; }
		}

		bool IList.IsFixedSize
		{
			get { return false; }
		}

		int IList.Add(object value)
		{
			VerifyValueType(value);

			lock (this._sync)
			{
				this.Add((T)value);
				return this.Count - 1;
			}
		}

		bool IList.Contains(object value)
		{
			VerifyValueType(value);
			return this.Contains((T)value);
		}

		int IList.IndexOf(object value)
		{
			VerifyValueType(value);
			return this.IndexOf((T)value);
		}

		void IList.Insert(int index, object value)
		{
			VerifyValueType(value);
			this.Insert(index, (T)value);
		}

		void IList.Remove(object value)
		{
			VerifyValueType(value);
			this.Remove((T)value);
		}

		private static void VerifyValueType(object value)
		{
			if (value == null)
			{
				if (typeof(T).IsValue())
				{
					throw new ArgumentException("value is null and a value type");
				}
			}
			else if (!(value is T))
			{
				throw new ArgumentException($"object is of type {value.GetType().FullName} but collection is of {typeof(T).FullName}");
            }
		}
	}
}
