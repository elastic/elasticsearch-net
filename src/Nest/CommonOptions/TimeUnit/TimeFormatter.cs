// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class TimeFormatter : IJsonFormatter<Time>
	{
		public Time Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String:
					return new Time(reader.ReadString());
				case JsonToken.Number:
					var milliseconds = reader.ReadInt64();
					if (milliseconds == -1)
						return Time.MinusOne;
					if (milliseconds == 0)
						return Time.Zero;

					return new Time(milliseconds);
				default:
					reader.ReadNextBlock();
					return null;
			}
		}

		public void Serialize(ref JsonWriter writer, Time value, IJsonFormatterResolver formatterResolver)
		{
			if (value == Time.MinusOne) writer.WriteInt32(-1);
			else if (value == Time.Zero) writer.WriteInt32(0);
			else if (value.Factor.HasValue && value.Interval.HasValue) writer.WriteString(value.ToString());
			else if (value.Milliseconds != null) writer.WriteInt64((long)value.Milliseconds);
		}
	}
}
