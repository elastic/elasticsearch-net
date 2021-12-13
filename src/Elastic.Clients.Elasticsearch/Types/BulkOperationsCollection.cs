// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	//public interface BulkOperationBase
	//{
	//	//BulkOperationBase Operation { get; set; }

	//	//TDocument Document { get; set; }
	//}

	//public sealed class BulkOperation<TDocument> : BulkOperationBase<TDocument>
	//{
	//	public BulkOperationBase Operation { get; set; }

	//	public TDocument Document { get; set; }
	//}

	public sealed class BulkIndexOperation<T> : BulkOperationBase
	{
		private static byte _newline => (byte)'\n';

		public BulkIndexOperation(T document) => Document = document;

		[JsonIgnore]
		public T Document { get; set; }

		protected override Type ClrType => typeof(T);

		protected override string Operation => "index";

		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperation<T>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			// TODO - We may be able to simplify here and simply serialise 'this' once we remove the ISelfSerializable etc.

			//internalWriter.WriteStartObject();

			//if (Index is not null)
			//{
			//	internalWriter.WritePropertyName("_index");

			//	if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			//	{
			//		JsonSerializer.Serialize(internalWriter, Index, dhls.Options);
			//	}
			//	else
			//	{
			//		JsonSerializer.Serialize(internalWriter, Index); // Unable to handle options if this were to ever be the case
			//	}
			//}

			//internalWriter.WriteEndObject();

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None) => throw new NotImplementedException();
	}

	public sealed class BulkIndexOperationDescriptor<T> : DescriptorBase<BulkIndexOperationDescriptor<T>>
	{
		private readonly T _document;

		private IndexName _index;

		public BulkIndexOperationDescriptor(T source) => _document = source;

		public BulkIndexOperationDescriptor<T> Index(IndexName index) => Assign(index, (a, v) => a._index = v);

		// We don't expect this descriptor to be directly serialised
		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			//var ms = new MemoryStream();
			//settings.RequestResponseSerializer.Serialize(this, ms);
			//ms.Position = 0;

			// TODO
		}

		internal BulkIndexOperation<T> ToBulkIndexOperation()
		{
			var operation = new BulkIndexOperation<T>(_document) { Index = _index ?? typeof(T) };

			return operation;
		}
	}

	public abstract class BulkOperationBase : IStreamSerializable
	{
		[JsonPropertyName("_id")]
		public Id Id { get; set; }

		[JsonPropertyName("_index")]
		public IndexName Index { get; set; }

		[JsonPropertyName("retry_on_conflict")]
		public int? RetriesOnConflict { get; set; }

		//public Routing Routing { get; set; }

		[JsonPropertyName("version")]
		public long? Version { get; set; }

		[JsonPropertyName("version_type")]
		public VersionType? VersionType { get; set; }

		protected abstract Type ClrType { get; }

		protected abstract string Operation { get; }

		protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None);

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) =>
			Serialize(stream, settings, formatting);

		Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) =>
			SerializeAsync(stream, settings, formatting);

		//Type BulkOperationBase.ClrType => ClrType;

		//string BulkOperationBase.Operation => Operation;

		//object BulkOperationBase.GetBody() => GetBody();

		//Id BulkOperationBase.GetIdForOperation(Inferrer inferrer) => GetIdForOperation(inferrer);

		//Routing BulkOperationBase.GetRoutingForOperation(Inferrer inferrer) => GetRoutingForOperation(inferrer);

		//protected abstract object GetBody();

		//protected virtual Id GetIdForOperation(Inferrer inferrer) => Id ?? new Id(GetBody());

		//protected virtual Routing GetRoutingForOperation(Inferrer inferrer) => Routing ?? new Routing(GetBody());
	}

	///// <summary>
	///// This class is used by <see cref="IBulkRequest.Operations" /> which needs thread safe adding <see cref="ICollection{T}.Add" /> as well as expose
	///// an equivalent of <see cref="List{T}.AddRange"/>. Because operations from Elasticsearch are executed in order none of the types in
	///// System.Collection.Concurrent can't be used for this. We need to preserve insert order and exposed indexed index because <see cref="BulkResponse.Items"/>
	///// is ordered and lines up with <see cref="BulkRequest.Operations"/> allowing one to zip the two together.
	///// </summary>
	///// <typeparam name="TOperation"></typeparam>
	public sealed class BulkOperationsCollection : IList<BulkOperationBase>, IList, IStreamSerializable
	//where TOperation : BulkOperationBase
	{
		private readonly object _lock = new();

		public BulkOperationsCollection() => Items = new List<BulkOperationBase>();

		public BulkOperationsCollection(IEnumerable<BulkOperationBase> operations)
		{
			Items = new List<BulkOperationBase>();
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

		public BulkOperationBase this[int index]
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

		bool ICollection<BulkOperationBase>.IsReadOnly => false;

		bool IList.IsReadOnly => false;

		bool ICollection.IsSynchronized => true;

		object IList.this[int index]
		{
			get => this[index];
			set
			{
				VerifyValueType(value);
				this[index] = (BulkOperationBase)value;
			}
		}

		private List<BulkOperationBase> Items { get; }

		object ICollection.SyncRoot => _lock;

		void ICollection.CopyTo(Array array, int index)
		{
			lock (_lock)
				((IList)Items).CopyTo(array, index);
		}

		public void Add(BulkOperationBase item)
		{
			lock (_lock)
				Items.Add(item);
		}

		public void Clear()
		{
			lock (_lock)
				Items.Clear();
		}

		public bool Contains(BulkOperationBase item)
		{
			lock (_lock)
				return Items.Contains(item);
		}

		public void CopyTo(BulkOperationBase[] array, int index)
		{
			lock (_lock)
				Items.CopyTo(array, index);
		}

		public bool Remove(BulkOperationBase item)
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

		public IEnumerator<BulkOperationBase> GetEnumerator()
		{
			lock (_lock)
				return Items.GetEnumerator();
		}

		int IList.Add(object value)
		{
			VerifyValueType(value);

			lock (_lock)
			{
				Add((BulkOperationBase)value);
				return Count - 1;
			}
		}

		bool IList.Contains(object value)
		{
			VerifyValueType(value);
			return Contains((BulkOperationBase)value);
		}

		int IList.IndexOf(object value)
		{
			VerifyValueType(value);
			return IndexOf((BulkOperationBase)value);
		}

		void IList.Insert(int index, object value)
		{
			VerifyValueType(value);
			Insert(index, (BulkOperationBase)value);
		}

		void IList.Remove(object value)
		{
			VerifyValueType(value);
			Remove((BulkOperationBase)value);
		}

		public static implicit operator BulkOperationsCollection(List<BulkOperationBase> items) => new(items);

		public int IndexOf(BulkOperationBase item)
		{
			lock (_lock)
				return InternalIndexOf(item);
		}

		public void Insert(int index, BulkOperationBase item)
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

		public void AddRange(IEnumerable<BulkOperationBase> items)
		{
			lock (_lock)
				Items.AddRange(items);
		}

		private int InternalIndexOf(BulkOperationBase item)
		{
			var count = Items.Count;

			for (var i = 0; i < count; i++)
			{
				if (Equals(Items[i], item))
					return i;
			}
			return -1;
		}

		private void InsertItem(int index, BulkOperationBase item) => Items.Insert(index, item);

		private void RemoveItem(int index) => Items.RemoveAt(index);

		private static void VerifyValueType(object value)
		{
			if (value == null)
			{
				if (typeof(BulkOperationBase).IsValueType)
					throw new ArgumentException("value is null and a value type");
			}
			else if (value is not BulkOperationBase)
				throw new ArgumentException($"object is of type {value.GetType().FullName} but collection is of {typeof(BulkOperationBase).FullName}");
		}

		public void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			foreach (var op in this)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				serializable.Serialize(stream, settings, formatting);
				stream.WriteByte((byte)'\n');
			}
		}

		public Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None) => throw new NotImplementedException();
	}
}
