// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(GeoBoundsConverter))]
public sealed partial class GeoBounds
{
}

internal sealed class GeoBoundsConverter :
	JsonConverter<GeoBounds>
{
	public override GeoBounds? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
	}

	public override void Write(Utf8JsonWriter writer, GeoBounds value, JsonSerializerOptions options)
	{
		if (value.TryGetCoordinates(out var coordinates))
		{
			writer.WriteValue(options, coordinates);
			return;
		}

		if (value.TryGetTopLeftBottomRight(out var topLeftBottomRight))
		{
			writer.WriteValue(options, topLeftBottomRight);
			return;
		}

		if (value.TryGetTopRightBottomLeft(out var topRightBottomLeft))
		{
			writer.WriteValue(options, topRightBottomLeft);
			return;
		}

		if (value.TryGetWkt(out var wkt))
		{
			writer.WriteValue(options, wkt);
			return;
		}

		throw new JsonException($"Unrecognized '{typeof(GeoBounds)}' variant.");
	}
}
