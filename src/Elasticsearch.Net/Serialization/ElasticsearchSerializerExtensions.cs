// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elasticsearch.Net.Extensions;
using Elasticsearch.Net.Utf8Json;

namespace Elasticsearch.Net
{
	public static class ElasticsearchSerializerExtensions
	{
		internal static void SerializeUsingWriter<T>(this IElasticsearchSerializer serializer, ref JsonWriter writer, T body, IConnectionConfigurationValues settings, SerializationFormatting formatting)
		{
			if (serializer is IInternalSerializer s && s.TryGetJsonFormatter(out var formatterResolver))
			{
				JsonSerializer.Serialize(ref writer, body, formatterResolver);
				return;
			}

			var memoryStreamFactory = settings.MemoryStreamFactory;
			var bodyBytes = serializer.SerializeToBytes(body, memoryStreamFactory, formatting);
			writer.WriteRaw(bodyBytes);
		}

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
