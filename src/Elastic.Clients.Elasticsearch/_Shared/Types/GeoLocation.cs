// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(GeoLocationConverter))]
public partial class GeoLocation
{
	public static bool IsValidLatitude(double latitude) => latitude >= -90 && latitude <= 90;

	public static bool IsValidLongitude(double longitude) => longitude >= -180 && longitude <= 180;
}

internal sealed class GeoLocationConverter :
	JsonConverter<GeoLocation>
{
	public override GeoLocation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new InvalidOperationException();
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
