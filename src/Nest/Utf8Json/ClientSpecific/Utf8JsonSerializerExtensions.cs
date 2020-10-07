// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Elastic.Transport.Extensions;
using Elasticsearch.Net;

namespace Nest.Utf8Json
{
	internal static class Utf8JsonSerializerExtensions
	{
		internal static void SerializeUsingWriter<T>(this ITransportSerializer serializer, ref JsonWriter writer, T body, IConnectionConfigurationValues settings, SerializationFormatting formatting)
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
