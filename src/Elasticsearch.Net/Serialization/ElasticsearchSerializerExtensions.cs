using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
	public static class ElasticsearchSerializerExtensions
	{
		public static byte[] SerializeToBytes<T>(
			this IElasticsearchSerializer serializer,
			T data,
			IMemoryStreamFactory memoryStreamFactory = null,
			SerializationFormatting formatting = SerializationFormatting.Indented
		)
		{
			memoryStreamFactory = memoryStreamFactory ?? RecyclableMemoryStreamFactory.Default;
			using (var ms = memoryStreamFactory.Create())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.ToArray();
			}
		}

		public static string SerializeToString<T>(
			this IElasticsearchSerializer serializer,
			T data,
			IMemoryStreamFactory memoryStreamFactory = null,
			SerializationFormatting formatting = SerializationFormatting.Indented
		)
		{
			memoryStreamFactory = memoryStreamFactory ?? RecyclableMemoryStreamFactory.Default;
			using (var ms = memoryStreamFactory.Create())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.Utf8String();
			}
		}


	}
}
