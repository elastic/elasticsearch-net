using System;
using Elasticsearch.Net;

namespace Nest
{
	internal class NullableTimeSpanToStringFormatter : IJsonFormatter<TimeSpan?>
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
