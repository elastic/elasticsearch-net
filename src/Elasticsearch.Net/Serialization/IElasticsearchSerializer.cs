using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IElasticsearchSerializer
	{
		object Deserialize(Type type, Stream stream);

		T Deserialize<T>(Stream stream);

		Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default);

		Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default);

		void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented);

		Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default
		);
	}
}
