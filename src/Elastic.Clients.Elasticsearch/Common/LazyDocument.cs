// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// A lazily deserialized document.
	/// </summary>
	[JsonConverter(typeof(LazyDocumentConverter))]
	public class LazyDocument
	{
		private readonly Serializer _sourceSerializer;
		private readonly Serializer _requestResponseSerializer;
		private readonly IMemoryStreamFactory _memoryStreamFactory;

		internal LazyDocument(byte[] bytes, IElasticsearchClientSettings settings)
		{
			Bytes = bytes;
			
			_sourceSerializer = settings.SourceSerializer;
			_requestResponseSerializer = settings.RequestResponseSerializer;
			_memoryStreamFactory = settings.MemoryStreamFactory;
		}

		internal byte[] Bytes { get; }

		internal T AsUsingRequestResponseSerializer<T>()
		{
			using var ms = _memoryStreamFactory.Create(Bytes);
			return _requestResponseSerializer.Deserialize<T>(ms);
		}

		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="LazyDocument" /> instance.
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		public T As<T>()
		{
			using var ms = _memoryStreamFactory.Create(Bytes);
			return _sourceSerializer.Deserialize<T>(ms);
		}

		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="LazyDocument" /> instance.
		/// </summary>
		/// <param name="objectType">The type</param>
		public object As(Type objectType)
		{
			using var ms = _memoryStreamFactory.Create(Bytes);
			return _sourceSerializer.Deserialize(objectType, ms);
		}

		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="LazyDocument" /> instance.
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		public ValueTask<T> AsAsync<T>(CancellationToken ct = default)
		{
			using var ms = _memoryStreamFactory.Create(Bytes);
			return _sourceSerializer.DeserializeAsync<T>(ms, ct);
		}

		/// <summary>
		/// Creates an instance of <paramref name="objectType" /> from this
		/// <see cref="LazyDocument" /> instance.
		/// </summary>
		/// <param name="objectType">The type</param>
		public ValueTask<object> AsAsync(Type objectType, CancellationToken ct = default)
		{
			using var ms = _memoryStreamFactory.Create(Bytes);
			return _sourceSerializer.DeserializeAsync(objectType, ms, ct);
		}
	}

	internal sealed class LazyDocumentConverter : JsonConverter<LazyDocument>
	{
		private readonly IElasticsearchClientSettings _settings;

		public LazyDocumentConverter(IElasticsearchClientSettings settings) => _settings = settings;

		public override LazyDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			using var jsonDoc = JsonSerializer.Deserialize<JsonDocument>(ref reader);
			using var stream = _settings.MemoryStreamFactory.Create();

			var writer = new Utf8JsonWriter(stream);
			jsonDoc.WriteTo(writer);
			writer.Flush();

			return new LazyDocument(stream.ToArray(), _settings);
		}

		public override void Write(Utf8JsonWriter writer, LazyDocument value, JsonSerializerOptions options) => throw new NotImplementedException();
	}
}
