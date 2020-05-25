// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Globalization;
using System.IO;
using System.Text;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;

namespace Nest
{
	internal class GeoLocationFormatter : IJsonFormatter<GeoLocation>
	{
		private static readonly AutomataDictionary Fields = new AutomataDictionary
		{
			{ "lat", 0 },
			{ "lon", 1 }
		};

		public GeoLocation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			switch (reader.GetCurrentJsonToken())
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String:
					var wkt = reader.ReadString();
					using (var tokenizer = new WellKnownTextTokenizer(new StringReader(wkt)))
					{
						var token = tokenizer.NextToken();
						if (token != TokenType.Word)
							throw new GeoWKTException(
								$"Expected word but found {tokenizer.TokenString()}", tokenizer.LineNumber, tokenizer.Position);

						var type = tokenizer.TokenValue.ToUpperInvariant();
						if (type != GeoShapeType.Point)
							throw new GeoWKTException(
								$"Expected {GeoShapeType.Point} but found {type}", tokenizer.LineNumber, tokenizer.Position);

						if (GeoWKTReader.NextEmptyOrOpen(tokenizer) == TokenType.Word)
							return null;

						var lon = GeoWKTReader.NextNumber(tokenizer);
						var lat = GeoWKTReader.NextNumber(tokenizer);
						return new GeoLocation(lat, lon) { Format = GeoFormat.WellKnownText };
					}
				default:
				{
					var count = 0;
					double lat = 0;
					double lon = 0;
					while (reader.ReadIsInObject(ref count))
					{
						var propertyName = reader.ReadPropertyNameSegmentRaw();
						if (Fields.TryGetValue(propertyName, out var value))
						{
							switch (value)
							{
								case 0:
									lat = reader.ReadDouble();
									break;
								case 1:
									lon = reader.ReadDouble();
									break;
							}
						}
						else
							reader.ReadNextBlock();
					}

					return new GeoLocation(lat, lon) { Format = GeoFormat.GeoJson };
				}
			}
		}

		public void Serialize(ref JsonWriter writer, GeoLocation value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			switch (value.Format)
			{
				case GeoFormat.GeoJson:
					writer.WriteBeginObject();
					writer.WritePropertyName("lat");
					writer.WriteDouble(value.Latitude);
					writer.WriteValueSeparator();
					writer.WritePropertyName("lon");
					writer.WriteDouble(value.Longitude);
					writer.WriteEndObject();
					break;
				case GeoFormat.WellKnownText:
					var lon = value.Longitude.ToString(CultureInfo.InvariantCulture);
					var lat = value.Latitude.ToString(CultureInfo.InvariantCulture);
					var length = GeoShapeType.Point.Length + lon.Length + lat.Length + 4;
					var builder = new StringBuilder(length)
						.Append(GeoShapeType.Point)
						.Append(" (")
						.Append(lon)
						.Append(" ")
						.Append(lat)
						.Append(")");
					writer.WriteString(builder.ToString());
					break;
			}
		}
	}
}
