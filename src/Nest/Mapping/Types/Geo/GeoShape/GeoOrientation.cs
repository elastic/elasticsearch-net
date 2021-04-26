/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	public enum GeoOrientation
	{
		ClockWise,
		CounterClockWise
	}

	internal class GeoOrientationFormatter : IJsonFormatter<GeoOrientation>
	{
		public void Serialize(ref JsonWriter writer, GeoOrientation value, IJsonFormatterResolver formatterResolver)
		{
			switch (value)
			{
				case GeoOrientation.ClockWise:
					writer.WriteString("cw");
					break;
				case GeoOrientation.CounterClockWise:
					writer.WriteString("ccw");
					break;
			}
		}

		public GeoOrientation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
			{
				// Default, complies with the OGC standard
				return GeoOrientation.CounterClockWise;
			}

			var enumString = reader.ReadString();
			switch (enumString.ToUpperInvariant())
			{
				case "LEFT":
				case "CW":
				case "CLOCKWISE":
					return GeoOrientation.ClockWise;
			}

			// Default, complies with the OGC standard
			return GeoOrientation.CounterClockWise;
		}
	}

	internal class NullableGeoOrientationFormatter : IJsonFormatter<GeoOrientation?>
	{
		public void Serialize(ref JsonWriter writer, GeoOrientation? value, IJsonFormatterResolver formatterResolver)
		{
			if (!value.HasValue)
			{
				writer.WriteNull();
				return;
			}

			switch (value)
			{
				case GeoOrientation.ClockWise:
					writer.WriteString("cw");
					break;
				case GeoOrientation.CounterClockWise:
					writer.WriteString("ccw");
					break;
			}
		}

		public GeoOrientation? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull())
			{
				return null;
			}

			var enumString = reader.ReadString();

			switch (enumString.ToUpperInvariant())
			{
				case "LEFT":
				case "CW":
				case "CLOCKWISE":
					return GeoOrientation.ClockWise;
				case "RIGHT":
				case "CCW":
				case "COUNTERCLOCKWISE":
					return GeoOrientation.CounterClockWise;
				default:
					return null;
			}
		}
	}
}
