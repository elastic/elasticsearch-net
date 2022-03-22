// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch
{
	/// <summary>
	/// <para>A lazily deserialized document.</para>
	/// <para>Holds raw JSON bytes which can be lazily converted to a specific <see cref="Type"/> at a later time.</para>
	/// </summary>
	[JsonConverter(typeof(LazyDocumentConverter))]
	public readonly struct LazyDocument
	{
		internal LazyDocument(byte[] bytes, IElasticsearchClientSettings settings)
		{
			Bytes = bytes;
			Settings = settings;
		}

		internal byte[]? Bytes { get; }
		internal IElasticsearchClientSettings? Settings { get; }

		/// <summary>
		/// Creates an instance of <typeparamref name="T" /> from this
		/// <see cref="LazyDocument" /> instance.
		/// </summary>
		/// <typeparam name="T">The type</typeparam>
		public T? As<T>()
		{
			if (Bytes is null || Settings is null || Bytes.Length == 0)
				return default;

			using var ms = Settings.MemoryStreamFactory.Create(Bytes);
			return Settings.SourceSerializer.Deserialize<T>(ms);
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

		public override void Write(Utf8JsonWriter writer, LazyDocument value, JsonSerializerOptions options) => throw new NotImplementedException("We only ever expect to deserialize a LazyDocument on responses.");
	}
}
