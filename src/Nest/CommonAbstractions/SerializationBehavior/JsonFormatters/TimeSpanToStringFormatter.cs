using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class TimeSpanToStringFormatter : IJsonFormatter<TimeSpan>
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

	//TODO: hook up in NestFormatterResolver to return TimeSpanToStringFormatter when StringTimeSpanAttribute is *not* specified on T
	internal class TimeSpanToStringFormatterResolver : IJsonFormatterResolver
	{
		public IJsonFormatter<T> GetFormatter<T>() => null;
	}
}
