using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A lazily deserialized document
	/// </summary>
	[JsonFormatter(typeof(LazyDocumentInterfaceFormatter))]
	public interface ILazyDocument
	{
		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		T As<T>();

		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <param name="objectType">The type</param>
		object As(Type objectType);

		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		Task<T> AsAsync<T>(CancellationToken ct = default);
		
		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="ILazyDocument" /> instance
		/// </summary>
		/// <param name="objectType">The type</param>
		Task<object> AsAsync(Type objectType, CancellationToken ct = default);
	}

	/// <inheritdoc />
	[JsonFormatter(typeof(LazyDocumentFormatter))]
	public class LazyDocument : ILazyDocument
	{
		private readonly IElasticsearchSerializer _serializer;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		internal LazyDocument(byte[] bytes, IJsonFormatterResolver formatterResolver) 
			: this(bytes, formatterResolver.GetConnectionSettings()) { }

		private LazyDocument(byte[] bytes, IConnectionSettingsValues settings) :
			this(bytes, settings.SourceSerializer, settings.MemoryStreamFactory) { }
		
		private LazyDocument(byte[] bytes, IElasticsearchSerializer serializer, IMemoryStreamFactory memoryStreamFactory)
		{
			Bytes = bytes;
			_serializer = serializer;
			_memoryStreamFactory = memoryStreamFactory;
		}


		internal byte[] Bytes { get; }

		/// <inheritdoc />
		public T As<T>()
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _serializer.Deserialize<T>(ms);
		}

		/// <inheritdoc />
		public object As(Type objectType)
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _serializer.Deserialize(objectType, ms);
		}
		
		/// <inheritdoc />
		public Task<T> AsAsync<T>(CancellationToken ct = default)
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _serializer.DeserializeAsync<T>(ms, ct);
		}

		/// <inheritdoc />
		public Task<object> AsAsync(Type objectType, CancellationToken ct = default)
		{
			using (var ms = _memoryStreamFactory.Create(Bytes))
				return _serializer.DeserializeAsync(objectType, ms, ct);
		}
	}
}
