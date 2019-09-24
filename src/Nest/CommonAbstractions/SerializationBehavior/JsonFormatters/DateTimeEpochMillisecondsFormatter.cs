using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class NullableDateTimeEpochMillisecondsFormatter : IJsonFormatter<DateTime?>
	{
		public DateTime? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			switch (token)
			{
				case JsonToken.String:
				{
					var formatter = formatterResolver.GetFormatter<DateTime>();
					return formatter.Deserialize(ref reader, formatterResolver);
				}
				case JsonToken.Null:
				{
					reader.ReadNext();
					return null;
				}
				case JsonToken.Number:
				{
					var millisecondsSinceEpoch = reader.ReadDouble();
					var dateTimeOffset = DateTimeUtil.Epoch.AddMilliseconds(millisecondsSinceEpoch);
					return dateTimeOffset.DateTime;
				}
				default:
					throw new Exception($"Cannot deserialize {nameof(DateTime)} from token {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, DateTime? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var dateTimeDifference = (value.Value - DateTimeUtil.Epoch).TotalMilliseconds;
			writer.WriteInt64((long)dateTimeDifference);
		}
	}
}
