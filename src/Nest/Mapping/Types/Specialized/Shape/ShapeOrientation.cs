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

using Nest.Utf8Json;

namespace Nest
{
	public enum ShapeOrientation
	{
		ClockWise,
		CounterClockWise
	}

	internal class ShapeOrientationFormatter : IJsonFormatter<ShapeOrientation>
	{
		public void Serialize(ref JsonWriter writer, ShapeOrientation value, IJsonFormatterResolver formatterResolver)
		{
			switch (value)
			{
				case ShapeOrientation.CounterClockWise:
					writer.WriteString("counterclockwise");
					break;
				case ShapeOrientation.ClockWise:
					writer.WriteString("clockwise");
					break;
			}
		}

		public ShapeOrientation Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			// Default
			if (reader.ReadIsNull())
				return ShapeOrientation.CounterClockWise;

			var enumString = reader.ReadString();
			switch (enumString.ToUpperInvariant())
			{
				case "CLOCKWISE":
				case "LEFT":
				case "CW":
					return ShapeOrientation.ClockWise;
			}

			// Default
			return ShapeOrientation.CounterClockWise;
		}
	}

	internal class NullableShapeOrientationFormatter : IJsonFormatter<ShapeOrientation?>
	{
		public void Serialize(ref JsonWriter writer, ShapeOrientation? value, IJsonFormatterResolver formatterResolver)
		{
			if (!value.HasValue)
			{
				writer.WriteNull();
				return;
			}

			switch (value)
			{
				case ShapeOrientation.CounterClockWise:
					writer.WriteString("counterclockwise");
					break;
				case ShapeOrientation.ClockWise:
					writer.WriteString("clockwise");
					break;
			}
		}

		public ShapeOrientation? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.ReadIsNull()) return null;

			var enumString = reader.ReadString();

			switch (enumString.ToUpperInvariant())
			{
				case "COUNTERCLOCKWISE":
				case "RIGHT":
				case "CCW":
					return ShapeOrientation.CounterClockWise;
				case "CLOCKWISE":
				case "LEFT":
				case "CW":
					return ShapeOrientation.ClockWise;
				default:
					return null;
			}
		}
	}
}
