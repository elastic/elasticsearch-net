using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{
	public interface IElasticsearchSerializer
	{
		object Deserialize(Type type, Stream stream);

		T Deserialize<T>(Stream stream);

		Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken));

		Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken));

		void Serialize<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented);

		Task SerializeAsync<T>(T data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented,
			CancellationToken cancellationToken = default(CancellationToken)
		);
	}

	public static class ElasticsearchSerializerExtensions
	{
		public static byte[] SerializeToBytes<T>(this IElasticsearchSerializer serializer, T data,
			SerializationFormatting formatting = SerializationFormatting.Indented
		)
		{
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.ToArray();
			}
		}

		public static string SerializeToString<T>(this IElasticsearchSerializer serializer, T data,
			SerializationFormatting formatting = SerializationFormatting.Indented
		)
		{
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.Utf8String();
			}
		}
	}
}
