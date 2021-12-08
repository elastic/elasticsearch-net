// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(GeoPointsConverter))]
public sealed class GeoPoints : List<GeoPoint>
{
	public GeoPoints() { }

	public GeoPoints(GeoPoint[] array) => AddRange(array);

	public static implicit operator GeoPoints(GeoPoint[] array) => new(array);
}

internal sealed class GeoPointsConverter : JsonConverter<GeoPoints>
{
	public override GeoPoints? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var geoPoints = new GeoPoints();

		if (reader.TokenType == JsonTokenType.StartArray)
		{
			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				var geoPoint = JsonSerializer.Deserialize<GeoPoint>(ref reader, options);
				geoPoints.Add(geoPoint);
			}
		}
		else
		{
			var geoPoint = JsonSerializer.Deserialize<GeoPoint>(ref reader, options);
			geoPoints.Add(geoPoint);
		}

		return geoPoints;
	}

	public override void Write(Utf8JsonWriter writer, GeoPoints value, JsonSerializerOptions options)
	{
		if (value is null || value.Count == 0)
		{
			writer.WriteNullValue();
			return;
		}

		if (value.Count == 1)
		{
			JsonSerializer.Serialize(writer, value[0], options);
			return;
		}

		writer.WriteStartArray();

		foreach (var point in value)
		{
			JsonSerializer.Serialize(writer, point, options);
		}

		writer.WriteEndArray();
	}
}

[JsonConverter(typeof(GeoPointConverter))]
public sealed class GeoPoint
{
	public GeoPoint(GeoLocation geoLocation)
	{
		Latitude = geoLocation.Latitude;
		Longitude = geoLocation.Longitude;
	}

	public GeoPoint(double latitude, double longitude)
	{
		Latitude = latitude;
		Longitude = longitude;
	}

	public GeoPoint(GeoHash geoHash)
	{
		if (string.IsNullOrEmpty(geoHash.Value))
			throw new ArgumentException("Invalid geo hash value.");

		Value = geoHash.Value;
	}

	internal GeoPoint(string value) => Value = value;

	internal double Latitude { get; set; }

	internal double Longitude { get; set; }

	internal string Value { get; set; }

	// TODO - Bounding box

	public static implicit operator GeoPoint(GeoLocation location) => new(location);
}

internal sealed class GeoPointConverter : JsonConverter<GeoPoint>
{
	public override GeoPoint? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.StartObject)
		{
			var geoLocation = JsonSerializer.Deserialize<GeoLocation>(ref reader, options);
			return new GeoPoint(geoLocation);
		}
		else if (reader.TokenType == JsonTokenType.String)
		{
			var geoPoint = new GeoPoint(reader.GetString());
			return geoPoint;
		}

		// TODO - Finish implementation

		return null;
	}

	public override void Write(Utf8JsonWriter writer, GeoPoint value, JsonSerializerOptions options)
	{
		if (!string.IsNullOrEmpty(value.Value))
		{
			writer.WriteStringValue(value.Value);
		}

		// TODO - Bounding box

		writer.WriteStartObject();
		writer.WritePropertyName("lat");
		writer.WriteNumberValue(value.Latitude);
		writer.WritePropertyName("lon");
		writer.WriteNumberValue(value.Longitude);
		writer.WriteEndObject();
	}
}

public struct GeoHash
{
	// Todo, IComparable etc.

	public GeoHash(string geoHash) => Value = geoHash;

	public string Value { get; }


}

[JsonConverter(typeof(GeoLocationConverter))]
public sealed class GeoLocation
{
	public GeoLocation(double latitude, double longitude)
	{
		Latitude = latitude;
		Longitude = longitude;
	}

	public GeoLocation(IEnumerable<double> latLonArray)
	{
		// Array geo-points are ordered as [ lon, lat ] to align with GeoJSON.

		var count = 0;

		foreach (var value in latLonArray)
		{
			count++;

			if (count > 2)
				throw new ArgumentException("Unexpected number of values.", nameof(latLonArray));

			switch (count)
			{
				case 1:
					Longitude = value;
					break;
				case 2:
					Latitude = value;
					break;
			}
		}
	}

	public double Latitude { get; }

	public double Longitude { get; }

	// TODO
}

internal sealed class GeoLocationConverter : JsonConverter<GeoLocation>
{
	public override GeoLocation? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		double? latitude = null;
		double? longitude = null;

		if (reader.TokenType == JsonTokenType.StartObject)
		{
			while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
			{
				if (reader.TokenType != JsonTokenType.PropertyName)
					continue;

				if (reader.ValueTextEquals("lat"))
				{
					reader.Read();
					latitude = reader.GetInt64();
					continue;
				}

				if (reader.ValueTextEquals("lon"))
				{
					reader.Read();
					longitude = reader.GetInt64();
					continue;
				}
			}
		}

		if (latitude.HasValue && longitude.HasValue)
		{
			return new GeoLocation(latitude.Value, longitude.Value);
		}

		// TODO - Complete this

		reader.Read();

		return null;
	}

	public override void Write(Utf8JsonWriter writer, GeoLocation value, JsonSerializerOptions options) => throw new NotImplementedException();
}

public sealed class GeoBoundingBox
{

}
