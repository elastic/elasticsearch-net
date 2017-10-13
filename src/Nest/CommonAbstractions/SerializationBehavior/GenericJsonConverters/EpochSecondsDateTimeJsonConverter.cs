using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	internal class EpochStringSecondsDateTimeJsonConverter : DateTimeConverterBase
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

				var dateTimeDifference = (dateTime.Value - Epoch).TotalSeconds;
				writer.WriteValue(dateTimeDifference.ToString());
				return;
			}

			var dateTimeOffsetDifference = (dateTimeOffset.Value - Epoch).TotalSeconds;
			writer.WriteValue(dateTimeOffsetDifference.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.String)
			{
				if (objectType == typeof(DateTimeOffset?) || objectType == typeof(DateTime?))
					return null;

				return objectType == typeof(DateTimeOffset)
					? default(DateTimeOffset)
					: default(DateTime);
			}

			var secondsSinceEpoch = Convert.ToDouble(reader.Value);
			var dateTimeOffset = Epoch.AddSeconds(secondsSinceEpoch);

			return objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?)
				? dateTimeOffset
				: dateTimeOffset.DateTime;
		}
	}

	internal class EpochSecondsDateTimeJsonConverter : DateTimeConverterBase
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

				var dateTimeDifference = (dateTime.Value - Epoch).TotalSeconds;
				writer.WriteValue(dateTimeDifference);
				return;
			}

			var dateTimeOffsetDifference = (dateTimeOffset.Value - Epoch).TotalSeconds;
			writer.WriteValue(dateTimeOffsetDifference);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Float && reader.TokenType != JsonToken.Integer)
			{
				if (objectType == typeof(DateTimeOffset?) || objectType == typeof(DateTime?))
					return null;

				return objectType == typeof(DateTimeOffset)
					? default(DateTimeOffset)
					: default(DateTime);
			}

			var secondsSinceEpoch = (double)reader.Value;
			var dateTimeOffset = Epoch.AddSeconds(secondsSinceEpoch);

			if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
				return dateTimeOffset;

			return dateTimeOffset.DateTime;
		}
	}
}
