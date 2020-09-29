// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;

namespace Nest
{
	internal class GeoCoordinateFormatter : IJsonFormatter<GeoCoordinate>
	{
		public GeoCoordinate Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginArray)
				return null;

			var doubles = formatterResolver.GetFormatter<double[]>()
				.Deserialize(ref reader, formatterResolver);
			switch (doubles.Length)
			{
				case 2:
					return new GeoCoordinate(doubles[1], doubles[0]);
				case 3:
					return new GeoCoordinate(doubles[1], doubles[0], doubles[2]);
				default:
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, GeoCoordinate value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginArray();
			writer.WriteDouble(value.Longitude);
			writer.WriteValueSeparator();
			writer.WriteDouble(value.Latitude);
			if (value.Z.HasValue)
			{
				writer.WriteValueSeparator();
				writer.WriteDouble(value.Z.Value);
			}
			writer.WriteEndArray();
		}
	}
}
