using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	// TODO: Add a resolver that uses Utf8Json formatters defined in this assembly
	public class LowLevelRequestResponseSerializer : IElasticsearchSerializer
	{
		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();

		public object Deserialize(Type type, Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(type.DefaultValue());
			return JsonSerializer.NonGeneric.Deserialize(type, stream, null);
		}

		public T Deserialize<T>(Stream stream)
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return default(T);
			return JsonSerializer.Deserialize<T>(stream, null);
		}

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(type.DefaultValue());
			return JsonSerializer.NonGeneric.DeserializeAsync(type, stream, null);
		}

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (stream == null || stream.CanSeek && stream.Length == 0) return Task.FromResult(default(T));
			return JsonSerializer.DeserializeAsync<T>(stream, null);
		}

		public void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented) =>
			JsonSerializer.Serialize(writableStream, data, null); // TODO: format indentation

		public Task SerializeAsync<T>(T data, Stream writableStream, SerializationFormatting formatting,
			CancellationToken cancellationToken = default(CancellationToken)) =>
			JsonSerializer.SerializeAsync(writableStream, data, null); // TODO: format indentation
	}
}
