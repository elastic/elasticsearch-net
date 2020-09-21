// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Elastic.Transport.Serialization;
using Elastic.Transport.Utf8Json;

namespace Nest
{
	internal static class Utf8JsonExtensions
	{
		public static void WriteSerialized<T>(this JsonWriter writer, T value, IElasticsearchSerializer serializer, IConnectionConfigurationValues settings, SerializationFormatting formatting = SerializationFormatting.None)
		{
			using var ms = settings.MemoryStreamFactory.Create();
			serializer.Serialize(value, ms, formatting);
			writer.WriteRaw(ms);
		}

	}
}
