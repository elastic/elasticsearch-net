// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;
using System.Text.Json;
using Elastic.Transport;

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// May be used by requests that need to serialise only part of their source rather than the request object itself.
/// </summary>
internal static class SourceSerialisation
{
	public static void Serialize<T>(T toSerialize, Utf8JsonWriter writer, SerializerBase sourceSerializer)
	{
		if (sourceSerializer is DefaultHighLevelSerializer defaultSerializer)
		{
			// When the serializer is our own which uses STJ we can avoid unneccesary allocations and serialise straight into the writer
			JsonSerializer.Serialize(writer, toSerialize, defaultSerializer.Options);
		}
		else
		{
			// We cannot use STJ since the implementation may use another serializer.
			// This path is a little less optimal
			using var ms = new MemoryStream();
			sourceSerializer.Serialize(toSerialize, ms);
			ms.Position = 0;
#if NET6_0_OR_GREATER
			writer.WriteRawValue(ms.GetBuffer());
#else
			// This is not super efficient but a variant on the suggestion at https://github.com/dotnet/runtime/issues/1784#issuecomment-608331125
			using var document = JsonDocument.Parse(ms);
			document.RootElement.WriteTo(writer);
#endif
		}
	}
}
