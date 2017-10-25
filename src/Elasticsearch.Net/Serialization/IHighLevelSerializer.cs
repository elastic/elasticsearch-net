using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net
{

	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(Stream stream);
		object Deserialize(Type type, Stream stream);

		Task<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default(CancellationToken));
		Task<object> DeserializeAsync(Type type, Stream stream, CancellationToken cancellationToken = default(CancellationToken));

		void Serialize(object data, Stream stream, SerializationFormatting formatting = SerializationFormatting.Indented);
	}


	public static class ElasticsearchSerializerExtensions
	{
		public static byte[] SerializeToBytes(this IElasticsearchSerializer serializer, object data, SerializationFormatting formatting = SerializationFormatting.Indented)
		{
			using (var ms = new MemoryStream())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.ToArray();
			}
		}
		public static string SerializeToString(this IElasticsearchSerializer serializer, object data, SerializationFormatting formatting = SerializationFormatting.Indented) =>
			serializer.SerializeToBytes(data, formatting).Utf8String();
	}
}
