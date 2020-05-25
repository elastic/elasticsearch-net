// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

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
			if (reader.ReadIsNull())
			{
				// Default
				return ShapeOrientation.CounterClockWise;
			}

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
			if (reader.ReadIsNull())
			{
				return null;
			}

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
