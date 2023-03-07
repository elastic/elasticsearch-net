// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch.Core.Bulk;

///// <summary>
///// This class is used by <see cref="IBulkRequest.Operations" /> which needs thread-safe adding <see cref="ICollection{T}.Add" />, as well as expose
///// an equivalent of <see cref="List{T}.AddRange"/>. Because operations from Elasticsearch are executed in order none of the types in
///// System.Collection.Concurrent can be used for this. We need to preserve insert order and exposed indexed index because <see cref="BulkResponse.Items"/>
///// is ordered and lines up with <see cref="BulkRequest.Operations"/> allowing one to zip the two together.
///// </summary>
///// <typeparam name="TOperation"></typeparam>
public sealed class BulkOperationsCollection : IList<IBulkOperation>, IList, IStreamSerializable
{
	private readonly object _lock = new();

	public BulkOperationsCollection() => Items = new List<IBulkOperation>();

	public BulkOperationsCollection(IEnumerable<IBulkOperation> operations)
	{
		Items = new List<IBulkOperation>();
		Items.AddRange(operations);
	}

	public int Count
	{
		get
		{
			lock (_lock)
				return Items.Count;
		}
	}

	public IBulkOperation this[int index]
	{
		get
		{
			lock (_lock)
				return Items[index];
		}
		set
		{
			lock (_lock)
			{
				if (index < 0 || index >= Items.Count)
					throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

				Items[index] = value;
			}
		}
	}

	bool IList.IsFixedSize => false;

	bool ICollection<IBulkOperation>.IsReadOnly => false;

	bool IList.IsReadOnly => false;

	bool ICollection.IsSynchronized => true;

	object IList.this[int index]
	{
		get => this[index];
		set
		{
			VerifyValueType(value);
			this[index] = (IBulkOperation)value;
		}
	}

	private List<IBulkOperation> Items { get; }

	object ICollection.SyncRoot => _lock;

	void ICollection.CopyTo(Array array, int index)
	{
		lock (_lock)
			((IList)Items).CopyTo(array, index);
	}

	public void Add(IBulkOperation item)
	{
		lock (_lock)
			Items.Add(item);
	}

	public void Clear()
	{
		lock (_lock)
			Items.Clear();
	}

	public bool Contains(IBulkOperation item)
	{
		lock (_lock)
			return Items.Contains(item);
	}

	public void CopyTo(IBulkOperation[] array, int index)
	{
		lock (_lock)
			Items.CopyTo(array, index);
	}

	public bool Remove(IBulkOperation item)
	{
		lock (_lock)
		{
			var index = InternalIndexOf(item);
			if (index < 0)
				return false;

			RemoveItem(index);
			return true;
		}
	}

	IEnumerator IEnumerable.GetEnumerator() => ((IList)Items).GetEnumerator();

	public IEnumerator<IBulkOperation> GetEnumerator()
	{
		lock (_lock)
			return Items.GetEnumerator();
	}

	int IList.Add(object value)
	{
		VerifyValueType(value);

		lock (_lock)
		{
			Add((IBulkOperation)value);
			return Count - 1;
		}
	}

	bool IList.Contains(object value)
	{
		VerifyValueType(value);
		return Contains((IBulkOperation)value);
	}

	int IList.IndexOf(object value)
	{
		VerifyValueType(value);
		return IndexOf((IBulkOperation)value);
	}

	void IList.Insert(int index, object value)
	{
		VerifyValueType(value);
		Insert(index, (IBulkOperation)value);
	}

	void IList.Remove(object value)
	{
		VerifyValueType(value);
		Remove((IBulkOperation)value);
	}

	public static implicit operator BulkOperationsCollection(List<IBulkOperation> items) => new(items);

	public int IndexOf(IBulkOperation item)
	{
		lock (_lock)
			return InternalIndexOf(item);
	}

	public void Insert(int index, IBulkOperation item)
	{
		lock (_lock)
		{
			if (index < 0 || index > Items.Count)
				throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

			InsertItem(index, item);
		}
	}

	public void RemoveAt(int index)
	{
		lock (_lock)
		{
			if (index < 0 || index >= Items.Count)
				throw new ArgumentOutOfRangeException("index", index, $"value {index} must be in range of {Items.Count}");

			RemoveItem(index);
		}
	}

	public void AddRange(IEnumerable<IBulkOperation> items)
	{
		lock (_lock)
			Items.AddRange(items);
	}

	private int InternalIndexOf(IBulkOperation item)
	{
		var count = Items.Count;

		for (var i = 0; i < count; i++)
		{
			if (Equals(Items[i], item))
				return i;
		}
		return -1;
	}

	private void InsertItem(int index, IBulkOperation item) => Items.Insert(index, item);

	private void RemoveItem(int index) => Items.RemoveAt(index);

	private static void VerifyValueType(object value)
	{
		if (value == null)
		{
			if (typeof(IBulkOperation).IsValueType)
				throw new ArgumentException("value is null and a value type");
		}
		else if (value is not IBulkOperation)
			throw new ArgumentException($"object is of type {value.GetType().FullName} but collection is of {typeof(IBulkOperation).FullName}");
	}

	public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		foreach (var op in this)
		{
			if (op is not IStreamSerializable serializable)
			{
				ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
				return;
			}

			serializable.Serialize(stream, settings, SerializationFormatting.None);
			stream.WriteByte((byte)'\n');
		}
	}

	public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
	{
		foreach (var op in this)
		{
			if (op is not IStreamSerializable serializable)
			{
				ThrowHelper.ThrowInvalidOperationForBulkWhenNotIStreamSerializable();
				return;
			}

			await serializable.SerializeAsync(stream, settings, SerializationFormatting.None).ConfigureAwait(false);
			stream.WriteByte((byte)'\n');
		}
	}
}
