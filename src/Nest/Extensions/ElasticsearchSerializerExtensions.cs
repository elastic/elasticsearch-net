// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.SharedExtensions;
using Elastic.Transport;
using Elastic.Transport.Serialization;
using Elastic.Transport.Utf8Json;
using JsonSerializer = Elastic.Transport.Utf8Json.JsonSerializer;

namespace Nest
{
	internal static class ElasticsearchSerializerExtensions
	{
		public static void SerializeUsingWriter<T>(this IElasticsearchSerializer serializer, ref JsonWriter writer, T body,
			IConnectionConfigurationValues settings, SerializationFormatting formatting
		)
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
	}
}
