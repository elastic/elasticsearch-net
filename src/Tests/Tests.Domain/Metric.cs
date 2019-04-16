using System;
using Elasticsearch.Net;

using Nest;

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
		[MachineLearningDateTime]
		[JsonFormatter(typeof(MachineLearningDateTimeFormatter))]
		public DateTime Timestamp { get; set; }

		public long Total { get; set; }
	}

	// Required as PreviewDatafeed API returns Timestamp values as epoch milliseconds, irrespective
	// of the format in which it was originally indexed.
	internal class MachineLearningDateTimeFormatter : IJsonFormatter<DateTime>
	{
		private static readonly DateTimeOffset Epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);

		public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var formatter = formatterResolver.GetFormatter<DateTime>();
				return formatter.Deserialize(ref reader, formatterResolver);
			}
			if (token == JsonToken.Null) return default;

			if (token == JsonToken.Number)
			{
				var millisecondsSinceEpoch = reader.ReadDouble();
				var dateTimeOffset = Epoch.AddMilliseconds(millisecondsSinceEpoch);
				return dateTimeOffset.DateTime;
			}

			throw new Exception($"Cannot deserialize {nameof(DateTimeOffset)} from token {token}");
		}

		public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver) =>
			ISO8601DateTimeFormatter.Default.Serialize(ref writer, value, formatterResolver);
	}
}
