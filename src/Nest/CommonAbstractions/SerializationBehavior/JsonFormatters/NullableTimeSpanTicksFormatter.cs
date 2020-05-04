// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class NullableTimeSpanTicksFormatter : IJsonFormatter<TimeSpan?>
	{
		public TimeSpan? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null:
					reader.ReadNext();
					return null;
				case JsonToken.String: return TimeSpan.Parse(reader.ReadString());
				case JsonToken.Number: return new TimeSpan(reader.ReadInt64());
			}
			throw new Exception($"Cannot convert token of type {token} to {nameof(TimeSpan)}?.");
		}

		public void Serialize(ref JsonWriter writer, TimeSpan? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
				writer.WriteNull();
			else
				writer.WriteInt64(value.Value.Ticks);
		}
	}
}
