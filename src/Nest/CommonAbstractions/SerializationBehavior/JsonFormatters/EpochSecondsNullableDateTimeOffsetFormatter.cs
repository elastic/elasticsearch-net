using System;
using Utf8Json;

namespace Nest
{
	internal class EpochSecondsNullableDateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset?>
	{
		public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Number)
				return null;

			var secondsSinceEpoch = reader.ReadDouble();
			var dateTimeOffset = DateTimeUtil.Epoch.AddSeconds(secondsSinceEpoch);
			return dateTimeOffset;
		}

		public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var dateTimeOffsetDifference = (value.Value - DateTimeUtil.Epoch).TotalSeconds;
			writer.WriteInt64((long)dateTimeOffsetDifference);
		}
	}
}
