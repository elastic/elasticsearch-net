using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>The built in internal serializer that the high level client NEST uses.</summary>
	internal class DefaultHighLevelSerializer : IElasticsearchSerializer, IInternalSerializerWithFormatter
	{
		public DefaultHighLevelSerializer(IJsonFormatterResolver formatterResolver) => FormatterResolver = formatterResolver;

		public IJsonFormatterResolver FormatterResolver { get; }

		public T Deserialize<T>(Stream stream)
		{
			return JsonSerializer.Deserialize<T>(stream, FormatterResolver);
		}

		public object Deserialize(Type type, Stream stream)
		{
			return JsonSerializer.NonGeneric.Deserialize(type, stream, FormatterResolver);
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			return JsonSerializer.DeserializeAsync<T>(stream, FormatterResolver);
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
		{
			return JsonSerializer.NonGeneric.DeserializeAsync(type, stream, FormatterResolver);
		}

		public virtual void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None) =>
			JsonSerializer.Serialize(writableStream, data, FormatterResolver);

		public Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.None,
			CancellationToken cancellationToken = default
		) => JsonSerializer.SerializeAsync(stream, data, FormatterResolver);
	}
}
