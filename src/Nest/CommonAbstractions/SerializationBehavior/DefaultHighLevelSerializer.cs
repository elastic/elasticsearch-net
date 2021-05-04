// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class DefaultHighLevelSerializer : IElasticsearchSerializer, IInternalSerializer
	{
		public DefaultHighLevelSerializer(IJsonFormatterResolver formatterResolver) => FormatterResolver = formatterResolver;

		private IJsonFormatterResolver FormatterResolver { get; }

		bool IInternalSerializer.TryGetJsonFormatter(out IJsonFormatterResolver formatterResolver)
		{
			formatterResolver = FormatterResolver;
			return true;
		}

		public T Deserialize<T>(Stream stream) =>
			JsonSerializer.Deserialize<T>(stream, FormatterResolver);

		public object Deserialize(Type type, Stream stream) =>
			JsonSerializer.NonGeneric.Deserialize(type, stream, FormatterResolver);

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.DeserializeAsync<T>(stream, FormatterResolver);

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default) =>
			JsonSerializer.NonGeneric.DeserializeAsync(type, stream, FormatterResolver);

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None) =>
			JsonSerializer.Serialize(writableStream, data, FormatterResolver);

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		) => JsonSerializer.SerializeAsync(stream, data, FormatterResolver);

	}
}
