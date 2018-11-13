using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utf8Json;
using Utf8Json.Resolvers;

namespace Elasticsearch.Net
{
	public class LowLevelRequestResponseSerializer : IElasticsearchSerializer
	{
		public static readonly LowLevelRequestResponseSerializer Instance = new LowLevelRequestResponseSerializer();

		public object Deserialize(Type type, Stream stream) =>
			JsonSerializer.NonGeneric.Deserialize(type, stream);

		public T Deserialize<T>(Stream stream) => JsonSerializer.Deserialize<T>(stream);

		public Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
			JsonSerializer.NonGeneric.DeserializeAsync(type, stream);

		public Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
			JsonSerializer.DeserializeAsync<T>(stream, null);

		public void Serialize<T>(T data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented) =>
			JsonSerializer.Serialize(writableStream, data); // TODO: format indentation

		public Task SerializeAsync<T>(T data, Stream writableStream, SerializationFormatting formatting,
			CancellationToken cancellationToken = default(CancellationToken)) =>
			JsonSerializer.SerializeAsync(writableStream, data); // TODO: format indentation
	}
}
