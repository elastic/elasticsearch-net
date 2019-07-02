using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
	public class LowLevelRequestResponseSerializer : IElasticsearchSerializer
	{
		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();

		public object Deserialize(Type type, Stream stream)
		{
			return JsonSerializer.NonGeneric.Deserialize(type, stream, ElasticsearchNetFormatterResolver.Instance);
		}

		public T Deserialize<T>(Stream stream)
		{
			return JsonSerializer.Deserialize<T>(stream, ElasticsearchNetFormatterResolver.Instance);
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default)
		{
			return JsonSerializer.NonGeneric.DeserializeAsync(type, stream, ElasticsearchNetFormatterResolver.Instance);
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
		{
			return JsonSerializer.DeserializeAsync<T>(stream, ElasticsearchNetFormatterResolver.Instance);
		}

		public void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.None) =>
			JsonSerializer.Serialize(writableStream, data, ElasticsearchNetFormatterResolver.Instance);

		public Task SerializeAsync<T>(T data, Stream writableStream, SerializationFormatting formatting,
			CancellationToken cancellationToken = default
		) =>
			JsonSerializer.SerializeAsync(writableStream, data, ElasticsearchNetFormatterResolver.Instance);
	}
}
