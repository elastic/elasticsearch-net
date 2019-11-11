using Elasticsearch.Net.Extensions;

namespace Elasticsearch.Net
{
	public static class ElasticsearchSerializerExtensions
	{
		/// <summary>
		/// Extension method that serializes an instance of <typeparamref name="T"/> to a byte array.
		/// </summary>
		public static byte[] SerializeToBytes<T>(
			this IElasticsearchSerializer serializer,
			T data,
			SerializationFormatting formatting = SerializationFormatting.None) =>
			SerializeToBytes(serializer, data, RecyclableMemoryStreamFactory.Default, formatting);

		/// <summary>
		/// Extension method that serializes an instance of <typeparamref name="T"/> to a byte array.
		/// </summary>
		/// <param name="memoryStreamFactory">
		/// A factory yielding MemoryStream instances, defaults to <see cref="RecyclableMemoryStreamFactory"/>
		/// that yields memory streams backed by pooled byte arrays.
		/// </param>
		public static byte[] SerializeToBytes<T>(
			this IElasticsearchSerializer serializer,
			T data,
			IMemoryStreamFactory memoryStreamFactory,
			SerializationFormatting formatting = SerializationFormatting.None
		)
		{
			memoryStreamFactory ??= RecyclableMemoryStreamFactory.Default;
			using (var ms = memoryStreamFactory.Create())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.ToArray();
			}
		}

		/// <summary>
		/// Extension method that serializes an instance of <typeparamref name="T"/> to a string.
		/// </summary>
		public static string SerializeToString<T>(
			this IElasticsearchSerializer serializer,
			T data,
			SerializationFormatting formatting = SerializationFormatting.None) =>
			SerializeToString(serializer, data, RecyclableMemoryStreamFactory.Default, formatting);

		/// <summary>
		/// Extension method that serializes an instance of <typeparamref name="T"/> to a string.
		/// </summary>
		/// <param name="memoryStreamFactory">
		/// A factory yielding MemoryStream instances, defaults to <see cref="RecyclableMemoryStreamFactory"/>
		/// that yields memory streams backed by pooled byte arrays.
		/// </param>
		public static string SerializeToString<T>(
			this IElasticsearchSerializer serializer,
			T data,
			IMemoryStreamFactory memoryStreamFactory,
			SerializationFormatting formatting = SerializationFormatting.None
		)
		{
			memoryStreamFactory ??= RecyclableMemoryStreamFactory.Default;
			using (var ms = memoryStreamFactory.Create())
			{
				serializer.Serialize(data, ms, formatting);
				return ms.Utf8String();
			}
		}
	}
}
