using System;
using Utf8Json;

namespace Nest
{
	internal class TimeSpanToStringConverter : IJsonFormatter<TimeSpan>
	{
		public TimeSpan Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.String: return TimeSpan.Parse(reader.ReadString());
				case JsonToken.Number: return new TimeSpan(reader.ReadInt64());
			}
			throw new Exception($"Cannot convert token of type {token} to {nameof(TimeSpan)}.");
		}

		public void Serialize(ref JsonWriter writer, TimeSpan value, IJsonFormatterResolver formatterResolver) => writer.WriteInt64(value.Ticks);
	}

	internal class NullableTimeSpanToStringConverter : IJsonFormatter<TimeSpan?>
	{
		public TimeSpan? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();
			switch (token)
			{
				case JsonToken.Null: return null;
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
