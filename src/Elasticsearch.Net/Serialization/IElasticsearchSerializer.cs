using Elasticsearch.Net.Connection;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Elasticsearch.Net.Serialization
{
	public interface IElasticsearchSerializer
	{
		T Deserialize<T>(Stream stream);

		Task<T> DeserializeAsync<T>(Stream responseStream, CancellationToken cancellationToken = default(CancellationToken));

		void Serialize(object data, Stream writableStream, SerializationFormatting formatting = SerializationFormatting.Indented);
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

	}

}