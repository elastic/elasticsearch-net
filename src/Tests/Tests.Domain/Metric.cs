using System;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests.Domain
{
	public class Metric
	{
		public long Accept { get; set; }

		public long Deny { get; set; }

		public string Host { get; set; }

		public float Response { get; set; }

		public string Service { get; set; }

		[Date(Name = "@timestamp")]
		[JsonConverter(typeof(TimestampJsonConverter))]
		public DateTime Timestamp { get; set; }

		public long Total { get; set; }
	}

	// Required as PreviewDatafeed API returns Timestamp values as epoch milliseconds, irrespective
	// of the format in which it was originally indexed.
	internal class TimestampJsonConverter : IsoDateTimeConverter
	{
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Integer)
			{
				var millisecondsSinceEpoch = Convert.ToDouble(reader.Value);
				var dateTimeOffset = Epoch.AddMilliseconds(millisecondsSinceEpoch);

				if (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?))
					return dateTimeOffset;

				return dateTimeOffset.DateTime;
			}

			return base.ReadJson(reader, objectType, existingValue, serializer);
		}
	}
}
