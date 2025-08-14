// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class GeoLocationConverter :
	JsonConverter<GeoLocation>
{
	// LatitudeLongitude.

	private static readonly JsonEncodedText PropLat = JsonEncodedText.Encode("lat");
	private static readonly JsonEncodedText PropLon = JsonEncodedText.Encode("lon");

	// GeoHash.

	private static readonly JsonEncodedText PropGeoHash = JsonEncodedText.Encode("geohash");

	public override GeoLocation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType is JsonTokenType.String)
		{
			return new GeoLocation(reader.GetString()!);
		}

		if (reader.TokenType is JsonTokenType.StartArray)
		{
			return new GeoLocation(reader.ReadCollectionValue<double>(options, null)!);
		}

		if (reader.TokenType is JsonTokenType.StartObject)
		{
			var readerSnapshot = reader;
			reader.Read();

			GeoLocation.Kind? kind = null;
			if (reader.TokenType is JsonTokenType.PropertyName)
			{
				if (reader.ValueTextEquals(PropLat) || reader.ValueTextEquals(PropLon))
				{
					kind = GeoLocation.Kind.LatitudeLongitude;
				}

				if (reader.ValueTextEquals(PropGeoHash))
				{
					kind = GeoLocation.Kind.GeoHash;
				}
			}

			reader = readerSnapshot;

			return kind switch
			{
				GeoLocation.Kind.LatitudeLongitude => new(reader.ReadValue<LatLonGeoLocation>(options)),
				GeoLocation.Kind.GeoHash => new(reader.ReadValue<GeoHashLocation>(options)),
				null => throw new JsonException($"Unrecognized '{typeof(GeoLocation)}' variant."),
				_ => throw new InvalidOperationException("unreachable")
			};
		}

		throw new JsonException($"Unrecognized '{typeof(GeoLocation)}' variant.");
	}

	public override void Write(Utf8JsonWriter writer, GeoLocation value, JsonSerializerOptions options)
	{
		if (value.TryGetCoordinates(out var coordinates))
		{
			writer.WriteCollectionValue(options, coordinates, null);
			return;
		}

		if (value.TryGetGeoHash(out var geoHash))
		{
			writer.WriteValue(options, geoHash);
			return;
		}

		if (value.TryGetLatitudeLongitude(out var latitudeLongitude))
		{
			writer.WriteValue(options, latitudeLongitude);
			return;
		}

		if (value.TryGetText(out var text))
		{
			writer.WriteValue(options, text);
			return;
		}

		throw new JsonException($"Unrecognized '{typeof(GeoLocation)}' variant.");
	}
}
