using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	internal class EpochMillisecondsDateTimeJsonConverter : DateTimeConverterBase
	{
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var dateTimeOffset = value as DateTimeOffset?;

			if (dateTimeOffset == null)
			{
				var dateTime = value as DateTime?;
				if (dateTime == null)
				{
					writer.WriteNull();
					return;
				}

				var dateTimeDifference = (dateTime.Value - Epoch).TotalMilliseconds;
				writer.WriteValue((long)dateTimeDifference);
				return;
			}

			var dateTimeOffsetDifference = (dateTimeOffset.Value - Epoch).TotalMilliseconds;
			writer.WriteValue((long)dateTimeOffsetDifference);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Integer)
			{
				if (objectType == typeof(DateTimeOffset?) || objectType == typeof(DateTime?))
					return null;

				if (objectType == typeof(DateTimeOffset))
					return default(DateTimeOffset);

				return default(DateTime);
			}

			var millisecondsSinceEpoch = Convert.ToDouble(reader.Value);
			var dateTimeOffset = Epoch.AddMilliseconds(millisecondsSinceEpoch);

			if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
				return dateTimeOffset;

			return dateTimeOffset.DateTime;
		}
	}
}
