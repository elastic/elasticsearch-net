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
using System.Threading;
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

	public interface IBulkOperation
	{
	}

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

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(Document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
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

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(Document, stream, formatting).ConfigureAwait(false);
		}
	}

	public sealed class BulkIndexOperationDescriptor<TSource> : BulkOperationDescriptorBase<BulkIndexOperationDescriptor<TSource>, TSource>
	{
		private static byte _newline => (byte)'\n';

		private readonly TSource _document;

		public BulkIndexOperationDescriptor(TSource source) => _document = source;

		protected override string Operation => "index";
		
		protected override void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			internalWriter.Flush();

			stream.WriteByte(_newline);

			settings.SourceSerializer.Serialize(_document, stream);
		}

		protected override async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default)
		{
			var requestResponseSerializer = settings.RequestResponseSerializer;

			var internalWriter = new Utf8JsonWriter(stream);

			internalWriter.WriteStartObject();
			internalWriter.WritePropertyName(Operation);

			if (requestResponseSerializer is DefaultHighLevelSerializer dhls)
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this, dhls.Options);
			}
			else
			{
				JsonSerializer.Serialize<BulkIndexOperationDescriptor<TSource>>(internalWriter, this); // Unable to handle options if this were to ever be the case
			}

			internalWriter.WriteEndObject();
			await internalWriter.FlushAsync().ConfigureAwait(false);

			stream.WriteByte(_newline);

			await settings.SourceSerializer.SerializeAsync(_document, stream).ConfigureAwait(false);
		}

		protected override void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			// TODO
		}
	}

	public abstract class BulkOperationDescriptorBase<T, TSource> : DescriptorBase<T>, IBulkOperation, IStreamSerializable where T : BulkOperationDescriptorBase<T, TSource>
	{
		private long? _version;
		private IndexName _index;

		protected abstract string Operation { get; }

		public BulkOperationDescriptorBase<T, TSource> Index(IndexName index) => Assign(index, (a, v) => a._index = v);

		public BulkOperationDescriptorBase<T, TSource> Version(long version) => Assign(version, (a, v) => a._version = v);

		protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
		{
			writer.WriteStartObject();

			SerializeInternal(writer, options, settings);

			if (_index is not null)
			{
				writer.WritePropertyName("_index");
				JsonSerializer.Serialize(writer, _index, options);
			}
			else
			{
				writer.WritePropertyName("_index");
				var index = settings.Inferrer.IndexName<TSource>();
				JsonSerializer.Serialize(writer, index, options);
			}

			if (_version.HasValue)
			{
				writer.WritePropertyName("version");
				JsonSerializer.Serialize(writer, _version.Value, options);
			}

			writer.WriteEndObject();
		}

		protected abstract void SerializeInternal(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings);

		protected abstract void Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting);

		protected abstract Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting, CancellationToken cancellationToken = default);

		void IStreamSerializable.Serialize(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => Serialize(stream, settings, formatting);

		Task IStreamSerializable.SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting) => SerializeAsync(stream, settings, formatting);
	}

	public abstract class BulkOperationBase : IBulkOperation, IStreamSerializable
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
	public sealed class BulkOperationsCollection : IList<IBulkOperation>, IList, IStreamSerializable
	//where TOperation : IBulkOperation
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
					throw new InvalidOperationException("");

				serializable.Serialize(stream, settings, formatting);
				stream.WriteByte((byte)'\n');
			}
		}

		public async Task SerializeAsync(Stream stream, IElasticsearchClientSettings settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			foreach (var op in this)
			{
				if (op is not IStreamSerializable serializable)
					throw new InvalidOperationException("");

				await serializable.SerializeAsync(stream, settings, formatting).ConfigureAwait(false);
				stream.WriteByte((byte)'\n');
			}
		}
	}
}
