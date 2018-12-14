using System;
using Utf8Json;

namespace Nest
{
	internal class EpochMillisecondsDateTimeOffsetFormatter : MachineLearningDateTimeOffsetFormatter
	{
		public override void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver)
		{
			var dateTimeOffsetDifference = (value - DateTimeUtil.Epoch).TotalMilliseconds;
			writer.WriteInt64((long)dateTimeOffsetDifference);
		}
	}

	internal class EpochMillisecondsNullableDateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset?>
	{
		public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var formatter = formatterResolver.GetFormatter<DateTimeOffset>();
				return formatter.Deserialize(ref reader, formatterResolver);
			}
			if (token == JsonToken.Null)
				return null;

			if (token == JsonToken.Number)
			{
				var millisecondsSinceEpoch = reader.ReadDouble();
				var dateTimeOffset = DateTimeUtil.Epoch.AddMilliseconds(millisecondsSinceEpoch);
				return dateTimeOffset;
			}

			throw new Exception($"Cannot deserialize {nameof(DateTimeOffset)} from token {token}");
		}

		public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var dateTimeOffsetDifference = (value.Value - DateTimeUtil.Epoch).TotalMilliseconds;
			writer.WriteInt64((long)dateTimeOffsetDifference);
		}
	}
}
