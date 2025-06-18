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
	// Coordinates.

	private static readonly JsonEncodedText PropBottom = JsonEncodedText.Encode("bottom");
	private static readonly JsonEncodedText PropLeft = JsonEncodedText.Encode("left");
	private static readonly JsonEncodedText PropRight = JsonEncodedText.Encode("right");
	private static readonly JsonEncodedText PropTop = JsonEncodedText.Encode("top");

	// TopLeftBottomRight.

	private static readonly JsonEncodedText PropBottomRight = JsonEncodedText.Encode("bottom_right");
	private static readonly JsonEncodedText PropTopLeft = JsonEncodedText.Encode("top_left");

	// TopRightBottomLeft.

	private static readonly JsonEncodedText PropBottomLeft = JsonEncodedText.Encode("bottom_left");
	private static readonly JsonEncodedText PropTopRight = JsonEncodedText.Encode("top_right");

	// WKT.

	private static readonly JsonEncodedText PropWkt = JsonEncodedText.Encode("wkt");

	public override GeoBounds? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		reader.ValidateToken(JsonTokenType.StartObject);

		var readerSnapshot = reader;
		reader.Read();

		GeoBounds.Kind? kind = null;
		if (reader.TokenType is JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(PropBottom) || reader.ValueTextEquals(PropLeft) ||
				reader.ValueTextEquals(PropRight) || reader.ValueTextEquals(PropTop))
			{
				kind = GeoBounds.Kind.Coordinates;
			}

			if (reader.ValueTextEquals(PropBottomRight) || reader.ValueTextEquals(PropTopLeft))
			{
				kind = GeoBounds.Kind.TopLeftBottomRight;
			}

			if (reader.ValueTextEquals(PropBottomLeft) || reader.ValueTextEquals(PropTopRight))
			{
				kind = GeoBounds.Kind.TopRightBottomLeft;
			}

			if (reader.ValueTextEquals(PropWkt))
			{
				kind = GeoBounds.Kind.Wkt;
			}
		}

		reader = readerSnapshot;

		return kind switch
		{
			GeoBounds.Kind.Coordinates => new(reader.ReadValue<CoordsGeoBounds>(options)),
			GeoBounds.Kind.TopLeftBottomRight => new(reader.ReadValue<TopLeftBottomRightGeoBounds>(options)),
			GeoBounds.Kind.TopRightBottomLeft => new(reader.ReadValue<TopRightBottomLeftGeoBounds>(options)),
			GeoBounds.Kind.Wkt => new(reader.ReadValue<WktGeoBounds>(options)),
			null => throw new JsonException($"Unrecognized '{typeof(GeoBounds)}' variant."),
			_ => throw new InvalidOperationException("unreachable")
		};
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
